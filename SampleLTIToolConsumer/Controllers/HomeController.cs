using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using OAuth;
using SampleLTIToolConsumer.Models;

namespace SampleLTIToolConsumer.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            //get user information from SIS
            var userId = "USERID";
            var userEmail = "user@tempuri.org";
            var userFirstName = "John";
            var userLastName = "Doe";
            var userFullName = "John Doe";

            //get integration information from Tool Provider (e.g. TaskStream)
            var targetUrl = "https://www.tempuri.org/target/url";
            var consumerKey = "consumerkey";
            var sharedSecret = "sharedsecret";

            var oauthBase = new OAuthBase();

            var vm = new OAuthRequestViewModel
                {
                    UserId = userId,
                    LisPersonContactEmailPrimary = userEmail,
                    LisPersonNameGiven = userFirstName,
                    LisPersonNameFamily = userLastName,
                    LisPersonNameFull = userFullName,

                    //these values come from the Tool Provider
                    OauthConsumerKey = consumerKey,
                    TargetUrl = targetUrl,

                    OauthTimestamp = oauthBase.GenerateTimeStamp(),
                    OauthNonce = oauthBase.GenerateNonce(),
                };

            //shared secret from tool provider
            vm.OauthSignature = generateSignature(oauthBase, vm, sharedSecret);

            return View(vm);
        }

        private string generateSignature(OAuthBase oauthBase, OAuthRequestViewModel vm, string sharedSecret)
        {
            var url = new StringBuilder(vm.TargetUrl);

            //if there were no querystring parameters and the last character of the url isn't a ?, add one
            //otherwise add an ampersand
            url.Append(url[url.Length - 1] != '?' ? '?' : '&');

            #region append all properties to query string

            url.AppendFormat("{0}={1}&", "context_id", string.IsNullOrEmpty(vm.ContextId) ? null : oauthBase.UrlEncode(vm.ContextId));
            url.AppendFormat("{0}={1}&", "context_label", string.IsNullOrEmpty(vm.ContextLabel) ? null : oauthBase.UrlEncode(vm.ContextLabel));
            url.AppendFormat("{0}={1}&", "context_title", string.IsNullOrEmpty(vm.ContextTitle) ? null : oauthBase.UrlEncode(vm.ContextTitle));
            url.AppendFormat("{0}={1}&", "context_type", string.IsNullOrEmpty(vm.ContextType) ? null : oauthBase.UrlEncode(vm.ContextType));
            url.AppendFormat("{0}={1}&", "ext_lms", string.IsNullOrEmpty(vm.ExtLms) ? null : oauthBase.UrlEncode(vm.ExtLms));
            url.AppendFormat("{0}={1}&", "launch_presentation_document_target", string.IsNullOrEmpty(vm.LaunchPresentationDocumentTarget) ? null : oauthBase.UrlEncode(vm.LaunchPresentationDocumentTarget));
            url.AppendFormat("{0}={1}&", "launch_presentation_locale", string.IsNullOrEmpty(vm.LaunchPresentationLocale) ? null : oauthBase.UrlEncode(vm.LaunchPresentationLocale));
            url.AppendFormat("{0}={1}&", "launch_presentation_return_url", string.IsNullOrEmpty(vm.LaunchPresentationReturnUrl) ? null : oauthBase.UrlEncode(vm.LaunchPresentationReturnUrl));
            url.AppendFormat("{0}={1}&", "lis_course_offering_sourcedid", string.IsNullOrEmpty(vm.LisCourseOfferingSourcedid) ? null : oauthBase.UrlEncode(vm.LisCourseOfferingSourcedid));
            url.AppendFormat("{0}={1}&", "lis_course_section_sourcedid", string.IsNullOrEmpty(vm.LisCourseSectionSourcedid) ? null : oauthBase.UrlEncode(vm.LisCourseSectionSourcedid));
            url.AppendFormat("{0}={1}&", "lis_person_contact_email_primary", string.IsNullOrEmpty(vm.LisPersonContactEmailPrimary) ? null : oauthBase.UrlEncode(vm.LisPersonContactEmailPrimary));
            url.AppendFormat("{0}={1}&", "lis_person_name_family", string.IsNullOrEmpty(vm.LisPersonNameFamily) ? null : oauthBase.UrlEncode(vm.LisPersonNameFamily));
            url.AppendFormat("{0}={1}&", "lis_person_name_full", string.IsNullOrEmpty(vm.LisPersonNameFull) ? null : oauthBase.UrlEncode(vm.LisPersonNameFull));
            url.AppendFormat("{0}={1}&", "lis_person_name_given", string.IsNullOrEmpty(vm.LisPersonNameGiven) ? null : oauthBase.UrlEncode(vm.LisPersonNameGiven));
            url.AppendFormat("{0}={1}&", "lti_message_type", string.IsNullOrEmpty(vm.LtiMessageType) ? null : oauthBase.UrlEncode(vm.LtiMessageType));
            url.AppendFormat("{0}={1}&", "lti_version", string.IsNullOrEmpty(vm.LtiVersion) ? null : oauthBase.UrlEncode(vm.LtiVersion));
            url.AppendFormat("{0}={1}&", "oauth_callback", string.IsNullOrEmpty(vm.OauthCallback) ? null : oauthBase.UrlEncode(vm.OauthCallback));
            url.AppendFormat("{0}={1}&", "oauth_consumer_key", string.IsNullOrEmpty(vm.OauthConsumerKey) ? null : oauthBase.UrlEncode(vm.OauthConsumerKey));
            url.AppendFormat("{0}={1}&", "oauth_nonce", string.IsNullOrEmpty(vm.OauthNonce) ? null : oauthBase.UrlEncode(vm.OauthNonce));
            url.AppendFormat("{0}={1}&", "oauth_signature", string.IsNullOrEmpty(vm.OauthSignature) ? null : oauthBase.UrlEncode(vm.OauthSignature));
            url.AppendFormat("{0}={1}&", "oauth_signature_method", string.IsNullOrEmpty(vm.OauthSignatureMethod) ? null : oauthBase.UrlEncode(vm.OauthSignatureMethod));
            url.AppendFormat("{0}={1}&", "oauth_timestamp", string.IsNullOrEmpty(vm.OauthTimestamp) ? null : oauthBase.UrlEncode(vm.OauthTimestamp));
            url.AppendFormat("{0}={1}&", "oauth_version", string.IsNullOrEmpty(vm.OauthVersion) ? null : oauthBase.UrlEncode(vm.OauthVersion));
            url.AppendFormat("{0}={1}&", "resource_link_id", string.IsNullOrEmpty(vm.ResourceLinkId) ? null : oauthBase.UrlEncode(vm.ResourceLinkId));
            url.AppendFormat("{0}={1}&", "resource_link_title", string.IsNullOrEmpty(vm.ResourceLinkTitle) ? null : oauthBase.UrlEncode(vm.ResourceLinkTitle));
            url.AppendFormat("{0}={1}&", "tool_consumer_info_product_family_code", string.IsNullOrEmpty(vm.ToolConsumerInfoProductFamilyCode) ? null : oauthBase.UrlEncode(vm.ToolConsumerInfoProductFamilyCode));
            url.AppendFormat("{0}={1}&", "tool_consumer_info_version", string.IsNullOrEmpty(vm.ToolConsumerInfoVersion) ? null : oauthBase.UrlEncode(vm.ToolConsumerInfoVersion));
            url.AppendFormat("{0}={1}&", "tool_consumer_instance_description", string.IsNullOrEmpty(vm.ToolConsumerInstanceDescription) ? null : oauthBase.UrlEncode(vm.ToolConsumerInstanceDescription));
            url.AppendFormat("{0}={1}&", "tool_consumer_instance_guid", string.IsNullOrEmpty(vm.ToolConsumerInstanceGuid) ? null : oauthBase.UrlEncode(vm.ToolConsumerInstanceGuid));
            url.AppendFormat("{0}={1}&", "tool_consumer_instance_name", string.IsNullOrEmpty(vm.ToolConsumerInstanceName) ? null : oauthBase.UrlEncode(vm.ToolConsumerInstanceName));
            url.AppendFormat("{0}={1}&", "tool_consumer_instance_url", string.IsNullOrEmpty(vm.ToolConsumerInstanceUrl) ? null : oauthBase.UrlEncode(vm.ToolConsumerInstanceUrl));
            url.AppendFormat("{0}={1}&", "user_id", string.IsNullOrEmpty(vm.UserId) ? null : oauthBase.UrlEncode(vm.UserId));


            #endregion

            string normalizedUrl;
            string normalizedRequestParameters;

            return oauthBase.GenerateSignature(new Uri(url.ToString()),
                vm.OauthConsumerKey,
                sharedSecret,
                null,
                null,
                "POST",
                vm.OauthTimestamp,
                vm.OauthNonce,
                out normalizedUrl, out normalizedRequestParameters);
        }

    }
}
