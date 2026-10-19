namespace Org.BouncyCastle.Pqc.Crypto.Lms
{
    /// <summary>The LMS and LM-OTS parameter set pair of an LMS key.</summary>
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

        /// <summary>Gets the LM sig parameters.</summary>
        public LMSigParameters LMSigParameters => m_lmSigParameters;

        /// <summary>Gets the LM ots parameters.</summary>
        public LMOtsParameters LMOtsParameters => m_lmOtsParameters;
    }
}
