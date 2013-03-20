using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SampleLTIToolConsumer.Models
{
    public class OAuthRequestViewModel
    {

        #region properties that never change

        public string LtiMessageType 
        {
            get { return "basic-lti-launch-request"; }
        }

        public string LtiVersion
        {
            get { return "LTI-1p0"; }
        }

        public string OauthCallback
        {
            get { return "about:blank"; }
        }

        public string OauthSignatureMethod 
        { 
            get { return "HMAC-SHA1"; }
        }

        public string OauthVersion 
        {
            get { return "1.0"; }
        }

        #endregion

        #region properties of the tool consumer
        /// <summary>
        /// Should reflect the name of the tool consumer, which is your application.
        /// </summary>
        public string ToolConsumerInfoProductFamilyCode { get; set; }
        public string ToolConsumerInfoVersion { get; set; }
        public string ToolConsumerInstanceDescription { get; set; }
        public string ToolConsumerInstanceGuid { get; set; }
        public string ToolConsumerInstanceName { get; set; }
        public string ToolConsumerInstanceUrl { get; set; }
        #endregion

        #region context properties
        public string ContextId { get; set; }
        public string ContextLabel { get; set; }
        public string ContextTitle { get; set; }
        public string ContextType { get; set; }
        public string ResourceLinkId { get; set; }
        public string ResourceLinkTitle { get; set; }
        #endregion

        #region launch presentation properties
        public string LaunchPresentationDocumentTarget { get; set; }
        public string LaunchPresentationLocale { get; set; }
        public string LaunchPresentationReturnUrl { get; set; }        
        #endregion

        #region lis related
        public string LisCourseOfferingSourcedid { get; set; }
        public string LisCourseSectionSourcedid { get; set; }
        #endregion        

        #region user specific properties
        public string LisPersonContactEmailPrimary { get; set; }
        public string LisPersonNameFamily { get; set; }
        public string LisPersonNameFull { get; set; }
        public string LisPersonNameGiven { get; set; }
        public string UserId { get; set; }
        #endregion

        /// <summary>
        /// The name of your tool consumer.
        /// </summary>
        public string ExtLms
        {
            get { return "SampleLTIToolConsumer"; }
        }

        public string OauthConsumerKey { get; set; }
        public string OauthNonce { get; set; }
        public string OauthSignature { get; set; }
        public string OauthTimestamp { get; set; }

        public string TargetUrl { get; set; }


    }
}