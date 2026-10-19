using System;

using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;

//import javax.crypto.interfaces.PBEKey;

namespace Org.BouncyCastle.Cms
{
    /// <summary>Base class for password based keys used with CMS password recipients (PKCS#5 scheme 2).</summary>
    /// <remarks>The password is wiped when the key is finalized.</remarks>
    public abstract class CmsPbeKey
        // TODO Create an equivalent interface somewhere?
        // : PBEKey
        : ICipherParameters
    {
        internal readonly char[] password;
        internal readonly byte[] salt;
        internal readonly int iterationCount;

        /// <summary>Create a key from a password, salt and iteration count.</summary>
        /// <param name="password">The password; it is copied.</param>
        /// <param name="salt">The salt; it is copied.</param>
        /// <param name="iterationCount">The iteration count.</param>
        public CmsPbeKey(
            char[] password,
            byte[] salt,
            int iterationCount)
        {
            this.password = (char[])password.Clone();
            this.salt = Arrays.Clone(salt);
            this.iterationCount = iterationCount;
        }

        /// <summary>Create a key from a password and a PBKDF2 key derivation algorithm identifier.</summary>
        /// <param name="password">The password; it is copied.</param>
        /// <param name="keyDerivationAlgorithm">The PBKDF2 algorithm identifier, carrying the salt and iteration count.
        /// </param>
        /// <exception cref="ArgumentException">The key derivation algorithm is not PBKDF2.</exception>
        public CmsPbeKey(
            char[] password,
            AlgorithmIdentifier keyDerivationAlgorithm)
        {
            if (!keyDerivationAlgorithm.Algorithm.Equals(PkcsObjectIdentifiers.IdPbkdf2))
                throw new ArgumentException("Unsupported key derivation algorithm: "
                    + keyDerivationAlgorithm.Algorithm);

            Pbkdf2Params kdfParams = Pbkdf2Params.GetInstance(
                keyDerivationAlgorithm.Parameters.ToAsn1Object());

            this.password = (char[])password.Clone();
            this.salt = kdfParams.GetSalt();
            // The count is attacker-supplied and unauthenticated (CMS EnvelopedData has no integrity gate before
            // the KEK is derived), so bound it before deriving; CPU-DoS guard shared with the PKCS#8/PBES2 path.
            this.iterationCount = PbeUtilities.CheckPbeIterationCount(kdfParams.IterationCountObject);
        }

#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        /// <summary>Create a key from a password, salt and iteration count.</summary>
        /// <param name="password">The password; it is copied.</param>
        /// <param name="salt">The salt; it is copied.</param>
        /// <param name="iterationCount">The iteration count.</param>
        public CmsPbeKey(ReadOnlySpan<char> password, ReadOnlySpan<byte> salt, int iterationCount)
        {
            this.password = password.ToArray();
            this.salt = salt.ToArray();
            this.iterationCount = iterationCount;
        }

        /// <summary>Create a key from a password and a PBKDF2 key derivation algorithm identifier.</summary>
        /// <param name="password">The password; it is copied.</param>
        /// <param name="keyDerivationAlgorithm">The PBKDF2 algorithm identifier, carrying the salt and iteration count.
        /// </param>
        /// <exception cref="ArgumentException">The key derivation algorithm is not PBKDF2.</exception>
        public CmsPbeKey(ReadOnlySpan<char> password, AlgorithmIdentifier keyDerivationAlgorithm)
        {
            if (!keyDerivationAlgorithm.Algorithm.Equals(PkcsObjectIdentifiers.IdPbkdf2))
                throw new ArgumentException("Unsupported key derivation algorithm: "
                    + keyDerivationAlgorithm.Algorithm);

            Pbkdf2Params kdfParams = Pbkdf2Params.GetInstance(keyDerivationAlgorithm.Parameters.ToAsn1Object());

            this.password = password.ToArray();
            this.salt = kdfParams.GetSalt();
            // The count is attacker-supplied and unauthenticated (CMS EnvelopedData has no integrity gate before
            // the KEK is derived), so bound it before deriving; CPU-DoS guard shared with the PKCS#8/PBES2 path.
            this.iterationCount = PbeUtilities.CheckPbeIterationCount(kdfParams.IterationCountObject);
        }
#endif

        ~CmsPbeKey()
        {
            // ZeroMemory (not Array.Clear) so the JIT cannot elide the wipe of the secret password
            // as a dead store; see the CLAUDE.md constant-time/zeroization guidance.
            Arrays.ZeroMemory(this.password);
        }

        /// <summary>Gets a copy of the salt.</summary>
        public byte[] Salt
        {
            get { return Arrays.Clone(salt); }
        }

        /// <summary>Gets the iteration count.</summary>
        public int IterationCount
        {
            get { return iterationCount; }
        }

        /// <summary>Gets the algorithm name, which is always "PKCS5S2".</summary>
        public string Algorithm
        {
            get { return "PKCS5S2"; }
        }

        /// <summary>Gets the format of the key, which is always "RAW".</summary>
        public string Format
        {
            get { return "RAW"; }
        }

        /// <summary>Returns null, as this key has no encoded form.</summary>
        public byte[] GetEncoded()
        {
            return null;
        }

        internal abstract KeyParameter GetEncoded(string algorithmOid);
    }
}
