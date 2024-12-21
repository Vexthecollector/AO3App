using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Webkit;

namespace AO3App
{
    public class CustomWebClient : WebViewClient
    {
       

        public override void OnPageFinished(WebView? view, string? url)
        {
           
            view?.EvaluateJavascript("(function() {document.getElementById('header').style.display = 'none';})()",null);
        }
    }
}
