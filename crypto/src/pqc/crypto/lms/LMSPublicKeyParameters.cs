using System;
using System.IO;

using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.IO;

namespace Org.BouncyCastle.Pqc.Crypto.Lms
{
    public sealed class LmsPublicKeyParameters
        : LmsKeyParameters, ILmsContextBasedVerifier
    {
        private LMSigParameters parameterSet;
        private LMOtsParameters lmOtsType;
        private byte[] I;
        private byte[] T1;

        /// <summary>Create a new <see cref="LmsPublicKeyParameters"/>.</summary>
        /// <param name="parameterSet">The parameter set.</param>
        /// <param name="lmOtsType">The lm ots type.</param>
        /// <param name="T1">The t 1.</param>
        /// <param name="I">The i.</param>
        public LmsPublicKeyParameters(LMSigParameters parameterSet, LMOtsParameters lmOtsType, byte[] T1, byte[] I)
            : base(false)
        {
            this.parameterSet = parameterSet;
            this.lmOtsType = lmOtsType;
            this.I = Arrays.Clone(I);
            this.T1 = Arrays.Clone(T1);
        }

        /// <summary>Return an <see cref="LmsPublicKeyParameters"/> from the given object.</summary>
        /// <param name="src">
        /// An <see cref="LmsPublicKeyParameters"/>, an object from which one can be constructed, or null.
        /// </param>
        /// <returns>The <see cref="LmsPublicKeyParameters"/>, or null if <paramref name="src"/> is null.</returns>
        public static LmsPublicKeyParameters GetInstance(object src)
        {
            if (src is LmsPublicKeyParameters lmsPublicKeyParameters)
                return lmsPublicKeyParameters;

            if (src is BinaryReader binaryReader)
                return Parse(binaryReader);

            if (src is Stream stream)
                return Parse(stream);

            if (src is byte[] bytes)
                return Parse(bytes);

            throw new ArgumentException($"cannot parse {src}");
        }

        internal static LmsPublicKeyParameters Parse(BinaryReader binaryReader)
        {
            LMSigParameters sigParameter = LMSigParameters.ParseByID(binaryReader);
            LMOtsParameters otsParameter = LMOtsParameters.ParseByID(binaryReader);

            byte[] I = BinaryReaders.ReadBytesFully(binaryReader, 16);

            byte[] T1 = BinaryReaders.ReadBytesFully(binaryReader, sigParameter.M);

            return new LmsPublicKeyParameters(sigParameter, otsParameter, T1, I);
        }

        internal static LmsPublicKeyParameters Parse(Stream stream) =>
            BinaryReaders.Parse(Parse, stream, leaveOpen: true);

        internal static LmsPublicKeyParameters Parse(byte[] buf) => Parse(buf, 0, buf.Length);

        internal static LmsPublicKeyParameters Parse(byte[] buf, int off, int len) =>
            BinaryReaders.Parse(Parse, buf, off, len, "LMS public key");

        /// <inheritdoc/>
        public override byte[] GetEncoded() => ToByteArray();

        /// <summary>Return the sig parameters.</summary>
        public LMSigParameters GetSigParameters() => parameterSet;

        /// <summary>Return the ots parameters.</summary>
        public LMOtsParameters GetOtsParameters() => lmOtsType;

        /// <summary>Return the lms parameters.</summary>
        public LmsParameters GetLmsParameters() => new LmsParameters(GetSigParameters(), GetOtsParameters());

        /// <summary>Return the t 1.</summary>
        public byte[] GetT1() => Arrays.Clone(T1);

        internal bool MatchesT1(byte[] sig) => Arrays.FixedTimeEquals(T1, sig);

        /// <summary>Return the i.</summary>
        public byte[] GetI() => Arrays.Clone(I);

        internal byte[] RefI() => I;

        /// <inheritdoc/>
        // TODO[api] Fix parameter name
        public override bool Equals(object o)
        {
            if (this == o)
                return true;

            return o is LmsPublicKeyParameters that
                && this.parameterSet.Equals(that.parameterSet)
                && this.lmOtsType.Equals(that.lmOtsType)
                && Arrays.AreEqual(this.I, that.I)
                && Arrays.AreEqual(this.T1, that.T1);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            int result = parameterSet.GetHashCode();
            result = 31 * result + lmOtsType.GetHashCode();
            result = 31 * result + Arrays.GetHashCode(I);
            result = 31 * result + Arrays.GetHashCode(T1);
            return result;
        }

        internal byte[] ToByteArray()
        {
            return Composer.Compose()
                .U32Str(parameterSet.ID)
                .U32Str(lmOtsType.ID)
                .Bytes(I)
                .Bytes(T1)
                .Build();
        }

        /// <inheritdoc/>
        public LmsContext GenerateLmsContext(byte[] signature)
        {
            try
            {
                return GenerateOtsContext(LmsSignature.GetInstance(signature));
            }
            catch (IOException e)
            {
                throw new IOException("cannot parse signature", e);
            }
        }

        internal LmsContext GenerateOtsContext(LmsSignature S)
        {
            int ots_typecode = GetOtsParameters().ID;
            if (S.OtsSignature.ParamType.ID != ots_typecode)
            {
                throw new ArgumentException("ots type from lsm signature does not match ots" +
                    " signature type from embedded ots signature");
            }

            return new LMOtsPublicKey(LMOtsParameters.GetParametersByID(ots_typecode), I,  S.Q, null)
                .CreateOtsContext(S);
        }

        /// <inheritdoc/>
        public bool Verify(LmsContext context) => Lms.VerifySignature(this, context);
    }
}
