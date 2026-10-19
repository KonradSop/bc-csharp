namespace Org.BouncyCastle.Pqc.Crypto.Lms
{
    public sealed class LmsParameters
    {
        private readonly LMSigParameters m_lmSigParameters;
        private readonly LMOtsParameters m_lmOtsParameters;

        /// <summary>Create a new <see cref="LmsParameters"/>.</summary>
        /// <param name="lmSigParameters">The lm sig parameters.</param>
        /// <param name="lmOtsParameters">The lm ots parameters.</param>
        public LmsParameters(LMSigParameters lmSigParameters, LMOtsParameters lmOtsParameters)
        {
            m_lmSigParameters = lmSigParameters;
            m_lmOtsParameters = lmOtsParameters;
        }

        public LMSigParameters LMSigParameters => m_lmSigParameters;

        public LMOtsParameters LMOtsParameters => m_lmOtsParameters;
    }
}
