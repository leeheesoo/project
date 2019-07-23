using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TweetSharp;

namespace KinderJoy.Site.Controllers
{
    [RoutePrefix("Twitter")]
    public class TwitterController : Controller
    {
        public string ConsumerKey { get; set; }
        public string ConsumerSecret { get; set; }

        public TwitterController()
        {
            ConsumerKey = ConfigurationManager.AppSettings["sns.twitter.consumerkey"];
            ConsumerSecret = ConfigurationManager.AppSettings["sns.twitter.consumersceret"];
        }

        [Route("LoginCheck")]
        public void LoginCheck()
        {
            string accessToken = (string)Session["twitter_accessToken"];
            if (accessToken == null)
            {
                Response.Redirect(Url.Action("Authorize"));
            }
            else
            {
                Response.Write("<script type=\"text/javascript\"> opener.twitterAfterLogin(true); window.close(); </script>");
            }
            Response.End();
        }


        [Route("Authorize")]
        public ActionResult Authorize()
        {
            // Pass your credentials to the service
            TwitterService service = new TwitterService(ConsumerKey, ConsumerSecret);



            // Step 1 - Retrieve an OAuth Request Token
            string url = "https://" + Request.ServerVariables["HTTP_HOST"] + Url.Action("AuthorizeCallback");
            OAuthRequestToken requestToken = service.GetRequestToken(url);

            // Step 2 - Redirect to the OAuth Authorization URL
            string uri = service.GetAuthorizationUri(requestToken).ToString();
            return new RedirectResult(uri);
        }


        [Route("AuthorizeCallback")]
        // This URL is registered as the application's callback at http://dev.twitter.com
        public string AuthorizeCallback(string oauth_token, string oauth_verifier)
        {
            var requestToken = new OAuthRequestToken { Token = oauth_token };

            // Step 3 - Exchange the Request Token for an Access Token
            TwitterService service = new TwitterService(ConsumerKey, ConsumerSecret);
            OAuthAccessToken accessToken = service.GetAccessToken(requestToken, oauth_verifier);

            // Step 4 - User authenticates using the Access Token
            service.AuthenticateWith(accessToken.Token, accessToken.TokenSecret);
            TwitterUser user = service.VerifyCredentials(new VerifyCredentialsOptions());

            Session["twitter_accessToken"] = accessToken.Token;
            Session["twitter_accessTokenSecret"] = accessToken.TokenSecret;
            Session["twitter_id"] = user.Id;
            Session["twitter_name"] = user.Name;
            Session["twitter_nick"] = user.ScreenName;
            Session["twitter_profile"] = user.ProfileImageUrl;

            return String.Format("<script type=\"text/javascript\"> opener.twitterAfterLogin(true); window.close(); </script>");
        }

        [Route("Share")]
        [HttpPost]
        public ActionResult Share(string status)
        {
            try
            {
                string accessToken = (string)(Session["twitter_accessToken"]);
                string aceessTokenSecret = (string)(Session["twitter_accessTokenSecret"]);
                var userId = Session["twitter_id"];
                string userName = (string)(Session["twitter_name"]);
                string userNickname = (string)(Session["twitter_nick"]);
                bool isValid = accessToken == null || aceessTokenSecret == null || userId == null || userName == null || userNickname == null;

                if (isValid)
                {
                    Response.StatusCode = 400;
                    return Json(new
                    {
                        Message = "트위터를 다시 로그인 해주세요"
                    });
                }

                TwitterService service = new TwitterService(ConfigurationManager.AppSettings["sns.twitter.consumerkey"], ConfigurationManager.AppSettings["sns.twitter.consumersceret"]);
                service.AuthenticateWith(accessToken, aceessTokenSecret);

                TwitterStatus twitterStatus = service.SendTweet(new SendTweetOptions
                {
                    Status = status
                });

                return Json(new
                {
                    Message = "정상적으로 처리 되었습니다.",
                    SnsId = userNickname,
                    SnsPostId = twitterStatus.IdStr
                });
            }
            catch (Exception)
            {
                return Json(new
                {
                    Message = "트위터는 하루에 한번만 공유 가능합니다."
                });
            }
        }
    }
}