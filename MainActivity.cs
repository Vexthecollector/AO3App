using Android.Graphics;
using Android.Views;
using Android.Webkit;
using Java.Lang;

namespace AO3App
{
    [Activity(Label = "@string/app_name", MainLauncher = true)]
    public class MainActivity : Activity
    {
        private float x1, x2;
        const int MIN_DISTANCE = 150;
        WebView webView;
        protected override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            LoadPage();
        }

        private async void LoadPage()
        {
            webView = (WebView)FindViewById(Resource.Id.webview);
            CustomWebClient client = new CustomWebClient();
            WebSettings webSettings = webView.Settings;
            webSettings.JavaScriptEnabled = true;
            webView.SetWebViewClient(client);
            webView.LoadUrl("https://archiveofourown.org/");
            webView.Reload();
        }

        public override bool OnKeyDown(Keycode keyCode, KeyEvent e)
        {
            // Check whether the key event is the Back button and if there's history.
            if ((keyCode == Keycode.Back) && webView.CanGoBack())
            {
                webView.GoBack();
                return true;
            }

            // If it isn't the Back button or there's no web page history, bubble up to
            // the default system behavior. Probably exit the activity.
            return base.OnKeyDown((Keycode)keyCode, e);
        }
        public override bool DispatchTouchEvent(MotionEvent? e)
        {
            switch (e.Action)
            {
                case MotionEventActions.Down:
                    x1 = e.GetX();
                    break;
                case MotionEventActions.Up:
                    x2 = e.GetX();
                    float deltaX = x2 - x1;
                    if (System.Math.Abs(deltaX) > MIN_DISTANCE)
                    {
                        if (webView.Url.Contains("chapters"))
                        {

                            if (x2 > x1)
                            {
                                //Swipe Left
                                webView.EvaluateJavascript("(function() {document.getElementsByClassName(\"chapter previous\")[0].children[0].click();})()", null);
                            }
                            else
                            {
                                //Swipe Right
                                webView.EvaluateJavascript("(function() {document.getElementsByClassName(\"chapter next\")[0].children[0].click();})()", null);
                            }
                        }
                    }
                    break;
            }

            return base.DispatchTouchEvent(e);
        }


        private async void LoadSideView()
        {

        }

    }
}