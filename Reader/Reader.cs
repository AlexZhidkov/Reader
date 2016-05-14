using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using HtmlAgilityPack;
using System.Speech.Synthesis;

namespace ReaderLib
{
    public class Reader
    {
        public void Read(Entry blogEntry)
        {
            using (var synth = new SpeechSynthesizer())
            {
                synth.SetOutputToDefaultAudioDevice();

                var builder = new PromptBuilder();
                builder.StartParagraph();
                builder.StartVoice(VoiceGender.Female);
                builder.AppendText(blogEntry.Title);
                builder.EndVoice();
                builder.EndParagraph();
                builder.AppendBreak();
                builder.StartParagraph();
                builder.StartVoice(VoiceGender.Male);
                builder.AppendText(blogEntry.Body);
                builder.EndVoice();
                builder.EndParagraph();
                builder.AppendBreak();

                synth.Speak(builder);
            }
        }

        public IEnumerable<Entry> GetBlog(string url)
        {
            var blog = new List<Entry>();
            HtmlWeb web = new HtmlWeb();
            HtmlDocument document = web.Load(url);
            //var document = new HtmlDocument();
            //document.LoadHtml(GetTestHtml());

            var entries = document.DocumentNode.Descendants("div").Where(x => x.Attributes.Contains("class") && x.Attributes["class"].Value.Equals("entry"));

            foreach (var entry in entries)
            {
                var title = "";
                var body = new StringBuilder();

                var entryHeader = entry.Descendants("h3").FirstOrDefault(x => x.Attributes.Contains("class") && x.Attributes["class"].Value.Contains("entry-header"));
                if (entryHeader != null)
                {
                    var firstTitle = entryHeader.Descendants("a").FirstOrDefault();
                    if (firstTitle != null)
                        title = firstTitle.InnerText;
                }

                var entryBody = entry.Descendants("div").FirstOrDefault(x => x.Attributes.Contains("class") && x.Attributes["class"].Value.Contains("entry-body"));
                if (entryBody != null)
                {
                    foreach (var paragraph in entryBody.Descendants("p"))
                    {
                        body.Append(paragraph.InnerText);
                        body.Append(' ');
                    }
                }

                blog.Add(new Entry(HttpUtility.HtmlDecode(title), HttpUtility.HtmlDecode(body.ToString())));
            }
            return blog;
        }

        private static string GetTestHtml()
        {
            string htmlCode;
            htmlCode = @"
<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN'
	'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>
<html xmlns='http://www.w3.org/1999/xhtml' id='typepad-standard'>
<head>
<!-- MAIN INDEX TEMPLATE -->
<meta name='viewport' content='width=device-width, initial-scale=1'>

<!-- google+1 -->
<meta itemprop='name' content='Seth's Blog'>
<meta itemprop='description' content=''>
<meta itemprop='image' content='http://sethgodin.typepad.com/btn.seth.plus.one.png'>

<meta http-equiv='Content-Type' content='text/html; charset=utf-8' />
<meta name='generator' content='http://www.typepad.com/' />


	<!-- js section -->
	<script language='javascript' src='/sg/js/functions.js'></script>
	<script src='http://www.google-analytics.com/urchin.js' type='text/javascript'></script>
	<!-- jquery master -->
	<script src='/sg/js/jquery-1.8.2.min.js' type='text/javascript'></script>
	<!-- caroufredsel functionality -->
	<script src='/sg/js/jquery.caroufredsel-6.1.0-packed.js' type='text/javascript'></script>
	<script type='text/javascript' language='javascript' src='/sg/js/helper-plugins/jquery.mousewheel.min.js'></script>
	<script type='text/javascript' language='javascript' src='/sg/js/helper-plugins/jquery.touchSwipe.min.js'></script>
	<!-- facebox functionality -->
	<script type='text/javascript' src='/sg/js/vert_facebox.js'></script>
	<script type='text/javascript'>
	jQuery(document).ready(function($) {
	  $('a[rel*=facebox]').facebox() 
	})
	</script>
	<!-- css section -->
	<link rel='stylesheet' href='/sg/css/facebox.css' />
	<link rel='stylesheet' href='/sg/css/sethgodin_vert_books.css' />

<script src='http://www.google-analytics.com/urchin.js' type='text/javascript'></script>
<script type='text/javascript'>
_uacct = 'UA-73751-1';
urchinTracker();
</script>

<script src='http://sethgodin.typepad.com/seths_blog/functions.js' type='text/javascript'></script>





<meta name='keywords' content='marketing, blog, seth, ideas, respect, permission' />
<meta name='description' content='Seth Godin's riffs on marketing, respect, and the ways ideas spread.' />
<link rel='alternate' media='handheld' href='http://sethgodin.typepad.com/.m/seths_blog/' />

<link rel='stylesheet' href='http://sethgodin.typepad.com/seths_blog/styles.css' type='text/css' media='screen' />
<link rel='stylesheet' href='http://static.typepad.com/.shared:ve5cf72d:typepad:en_us/themes/common/print.css' type='text/css' media='print' />
<link rel='stylesheet' href='http://sethgodin.typepad.com/seths_blog/smart-nav.css' type='text/css' media='print' />


<link rel='alternate' type='application/atom+xml' title='Posts on Seth's Blog (Atom)' href='http://sethgodin.typepad.com/seths_blog/atom.xml' />


<title>Seth's Blog</title>

<link rel='openid.server' href='http://www.typepad.com/services/openid/server' />
<link rel='EditURI' type='application/rsd+xml' title='RSD' href='http://www.typepad.com/t/rsd/3511' />


	<link rel='meta' type='application/rdf+xml' title='FOAF' href='http://sethgodin.typepad.com/foaf.rdf' />


</head>

<body class='layout-two-column-left'>

<div id='container'>
	<div id='container-inner' class='pkg'>
	
	<!-- banner -->
	<div id='banner'>
	<div id='banner-inner' class='pkg'>
		<h1 id='banner-header'><a href='http://sethgodin.typepad.com/seths_blog/' accesskey='1'>Seth's Blog</a></h1>
		<h2 id='banner-description'>...Seth Godin's riffs on marketing, respect, and the ways ideas spread.</h2>
	</div><!-- #banner-inner -->
	</div><!-- #banner -->
	
	<!-- navigation bar is included from a module, so you can edit it once and reuse it across your blog -->
	<!--div id='nav'>
<div id='nav-inner'>
<ul class='nav-list pkg'>
	<li class='nav-list-item'><a href='http://sethgodin.typepad.com/seths_blog/'>Home</a></li>
	<li class='nav-list-item'><a href='http://sethgodin.typepad.com/about.html'>About</a></li>
	<li class='nav-list-item'><a href='http://sethgodin.typepad.com/seths_blog/archives.html'>Archives</a></li>
	<li class='nav-list-item'><a href='http://sethgodin.typepad.com/seths_blog/atom.xml'>Subscribe</a></li>
</ul>
</div>
</div-->

	
	<div id='sb-smart-nav'><div id='sb-smart-background' onclick='document.getElementById('ssn-open').style.backgroundColor='#ffffff'; document.getElementById('ssn-close').style.display='none';document.getElementById('sb-smart-nav-content').style.display='none'; document.getElementById('sb-smart-background').style.display='none'; return false;'></div>
<div id='sb-smart-nav-open'><!-- smart device nav system: Onramp | PAR -->
<div id='ssn-open'><a href='#' id='a-ssn-open' name='open' onclick='document.getElementById('ssn-open').style.backgroundColor='#fe9901'; document.getElementById('sb-smart-nav-content').style.top = $(window).scrollTop() + 36 + 'px'; document.getElementById('sb-smart-nav-content').style.display='inline'; document.getElementById('ssn-close').style.display='inline'; document.getElementById('sb-smart-background').style.display='inline'; return false;'></a></div>
<div id='ssn-close'><a href='#' id='a-ssn-close' name='close' onclick='document.getElementById('ssn-open').style.backgroundColor='#ffffff';  document.getElementById('ssn-close').style.display='none';document.getElementById('sb-smart-nav-content').style.display='none'; document.getElementById('sb-smart-background').style.display='none'; return false;'></a></div></div>
<div id='sb-smart-nav-content'><div id='ssn-content'>
<!-- sb links -->
<div class='sb-module-links module'>
	<h2 class='module-header'>Links</h2>
	<div class='module-content'>
		<ul class='module-list'>
			<li class='module-list-item'><a href='http://www.sethgodin.com'>Seth's Main Site</a></li>
			<li class='module-list-item'><a href='http://www.sethgodin.com/sg/books.asp'>Seth's Books</a></li>
			<li class='module-list-item'><a href='http://www.sethgodin.com/sg/subscribe.aspx'>Subscribe to Seth's Blog</a></li>
		</ul>
	</div><!-- .module-content -->
</div><!-- .module-links .module -->

	<div class='module-archives module'>
		<h2 class='module-header'>Recent Posts</h2>
		<div class='module-content'>
			<ul class='module-list'>
				
					
					<li class='module-list-item'><a href='http://sethgodin.typepad.com/seths_blog/2016/05/striking-a-chord.html'>Striking a chord</a></li>
					
				
					
					<li class='module-list-item'><a href='http://sethgodin.typepad.com/seths_blog/2016/05/the-problem-you-cant-talk-about.html'>The problem you can't talk about</a></li>
					
				
					
					<li class='module-list-item'><a href='http://sethgodin.typepad.com/seths_blog/2016/05/avoid-being-treated-like-a-child.html'>On being treated like an adult</a></li>
					
				
					
					<li class='module-list-item'><a href='http://sethgodin.typepad.com/seths_blog/2016/05/rigor.html'>Rigor</a></li>
					
				
					
					<li class='module-list-item'><a href='http://sethgodin.typepad.com/seths_blog/2016/05/calling-your-finding.html'>Calling your finding</a></li>
					
				
					
					<li class='module-list-item'><a href='http://sethgodin.typepad.com/seths_blog/2016/05/unlimited-bowling.html'>Unlimited bowling</a></li>
					
				
					
					<li class='module-list-item'><a href='http://sethgodin.typepad.com/seths_blog/2016/05/the-most-common-b2b-objection.html'>The most common b2b objection (and the one we have about most innovations)</a></li>
					
				
					
					<li class='module-list-item'><a href='http://sethgodin.typepad.com/seths_blog/2016/05/what-do-i-owe-you.html'>'What do I owe you?'</a></li>
					
				
					
					<li class='module-list-item'><a href='http://sethgodin.typepad.com/seths_blog/2016/05/learning-from-the-rejection.html'>Learning from the rejection</a></li>
					
				
					
					<li class='module-list-item'><a href='http://sethgodin.typepad.com/seths_blog/2016/05/duck.html'>Duck!</a></li>
					
				
			</ul>
		</div>
	</div>

<!-- sb monthly archives -->
<div class='sb-module-archives module'>
<h2 class='module-header'><a href='http://sethgodin.typepad.com/seths_blog/archives.html'>Archives</a></h2>
<div class='module-content'>
	
			
					<ul class='module-list'>
			
			<li class='module-list-item'><a href='http://sethgodin.typepad.com/seths_blog/2016/05/index.html'>May 2016</a></li>
			
	
			
			<li class='module-list-item'><a href='http://sethgodin.typepad.com/seths_blog/2016/04/index.html'>April 2016</a></li>
			
	
			
			<li class='module-list-item'><a href='http://sethgodin.typepad.com/seths_blog/2016/03/index.html'>March 2016</a></li>
			
	
			
			<li class='module-list-item'><a href='http://sethgodin.typepad.com/seths_blog/2016/02/index.html'>February 2016</a></li>
			
	
			
			<li class='module-list-item'><a href='http://sethgodin.typepad.com/seths_blog/2016/01/index.html'>January 2016</a></li>
			
	
			
			<li class='module-list-item'><a href='http://sethgodin.typepad.com/seths_blog/2015/12/index.html'>December 2015</a></li>
			
	
			
			<li class='module-list-item'><a href='http://sethgodin.typepad.com/seths_blog/2015/11/index.html'>November 2015</a></li>
			
	
			
			<li class='module-list-item'><a href='http://sethgodin.typepad.com/seths_blog/2015/10/index.html'>October 2015</a></li>
			
	
			
			<li class='module-list-item'><a href='http://sethgodin.typepad.com/seths_blog/2015/09/index.html'>September 2015</a></li>
			
	
			
			<li class='module-list-item'><a href='http://sethgodin.typepad.com/seths_blog/2015/08/index.html'>August 2015</a></li>
			
					</ul>
			
				<p class='module-more'><a href='http://sethgodin.typepad.com/seths_blog/archives.html'>More...</a></p>
			
			
	
</div><!-- .module-content -->
</div><!-- .module-archives .module -->

</div></div></div>
	
	<div id='pagebody'>
		<div id='pagebody-inner' class='pkg'>
		
		<div id='alpha'>
			<div id='alpha-inner' class='pkg'>
			
			<!-- include sidebar module content here -->
			
			<div class='module-typelist module' style='padding-top: 0px; margin-top: 0px; margin-bottom: 10px; padding-top: 10px;'>
	<div class='module-content' style='padding-top: 0px; margin-top: 0px;'>
		<div id='photo'><A href='http://sethgodin.typepad.com' title='Click here for more options.' onclick='MM_showHideAllLayers(); MM_showHideLayers('InfoLayer1','','show'); hideInXSeconds('InfoLayer1'); return false;'><img src='http://sethgodin.typepad.com/icn.seths.head.png' width='174' height='333' border='0'/></A><br>
			<div id='InfoLayer1' style='position:absolute; width:150px; height: 201px; z-index:200000 ;margin: -225px 0 0 0; padding: 10px 10px 10px 10px; visibility: hidden; background-color: #ffffff; border: 2px solid #EEEEEE;'>
				<div id='sidenav_more_seth'>
				<div id='more_seth_links'>
				For More Seth check out these links:</strong><br>
			    <ul style='margin: 0 0 0 5px; padding: 5px 5px 5px 5px; list-style-type : none; display: block;'>
					<li style='display: block; padding: 5px 0 0 0;'><a href='http://www.sethgodin.com/sg/subscribe.asp' style='display: block;'>Subscribe</a></li>
					<li style='display: block; padding: 5px 0 0 0;'><a href='http://sethgodin.typepad.com' style='display: block;'>Back to the home page for this blog</a></li>
					<li style='display: block; padding: 5px 0 0 0;'><a href='http://www.sethgodin.com/sg/books.asp' style='display: block;'>Check out Seth's books</a></li>
					<li style='display: block; padding: 5px 0 0 0;'><a href='http://sethgodin.typepad.com/seths_blog/archives.html' style='display: block;'>Visit the archives (more than 2,500 posts)</a></li>
					<li style='display: block; padding: 5px 0 0 0;'><a href='http://www.squidoo.com/seth/' style='display: block;'>Seth on Squidoo</a></li>
					<li style='display: block; padding: 5px 0 0 0;'><a href='https://plus.google.com/106497949182730964838?rel=author' style='display: block;'>Seth on Google</a></li>
					<li style='display: block; padding: 5px 0 0 0;'><a href='http://en.wikipedia.org/wiki/Seth_Godin' style='display: block;'>Seth at Wikipedia</a></li>
			    </ul>
				</div>
				<!--div id='more_seth_close'><a href='http://sethgodin.typepad.com' onClick='MM_showHideAllLayers(); return false;'><img src='http://sethgodin.typepad.com/icon_close.gif' title='close' alt='close' width='11' height='11' hspace='0' vspace='0' border='0' align='right'></a></div-->
				</div>
			</div>
		</div>
	</div>
</div>

			
			<div class='module-archives module' style=' margin-bottom: 0px; padding-bottom: 0px;'>
<h2 class='module-header'>Don't Miss a Thing<br />Free Updates by Email</h2>
<div class='module-content' style=' margin-bottom: 0px; padding-bottom: 0px;'>
<form Method='POST' action='http://www.feedblitz.com/f/f.fbz?AddNewUserDirect' style=' margin-bottom: 0px; padding-bottom: 0px;'>
<input name='FEEDID' type='hidden' value='198516' />
<p>Enter your email address</p>
<input name='EMAIL' type='text' value='me@email.com' maxlength='255' class='sidenav_input_text' onfocus='value='''><br><input type='submit' value='subscribe' class='sidenav_button'>
<p><a style='margin-left: 7px; font-size: 10px;  text-decoration: none; text-transform: lowercase;'  href='http://www.feedblitz.com/f?previewfeed=198516'>preview</a>&nbsp;&nbsp;|&nbsp;&nbsp;powered by <a  style='font-size: 10px; color: #666666; text-decoration: none;' href='http://www.feedblitz.com'>FeedBlitz</a></p>
</form>
</div>
</div>
			
			<!-- RSS Feeds TypeList -->
<div class='module-typelist module'>
	<h2 class='module-header'>RSS Feeds</h2>
	<div class='module-content'>
	<!-- AddThis Button BEGIN -->
	<div class='addthis_toolbox addthis_default_style '>
	<a href='http://www.addthis.com/bookmark.php?v=300&amp;username=xa-4d559ebf30e58c07' class='addthis_button_compact'>Share</a>
	<span class='addthis_separator'>|</span>
	<a class='addthis_button_preferred_1'></a>
	<a class='addthis_button_preferred_2'></a>
	<a class='addthis_button_preferred_3'></a>
	<a class='addthis_button_preferred_4'></a>
	</div>
	<script type='text/javascript' src='http://s7.addthis.com/js/300/addthis_widget.js#username=xa-4d559ebf30e58c07'></script>
	<!-- AddThis Button END -->
	<p>
	<!-- AddThis Feed Button BEGIN -->
	<a style='border: none;' href='http://www.addthis.com/feed.php?h1=http%3A%2F%2Ffeeds.feedblitz.com%2FSethsBlog&amp;pub=sethgodin' title='Subscribe to feed using any feed reader!'><img src='http://www.addme.com/images/button0-fd.gif' width='83' height='16' border='0'/></a>
	<!-- AddThis Feed Button END -->
	<!-- AddThis Bookmark Button BEGIN -->
	<a   style='border: none;'  href='javascript: location.href='http://www.addthis.com/bookmark.php?pub=sethgodin&amp;url='+encodeURIComponent(location.href)+'&amp;title='+encodeURIComponent(document.title);' title='Bookmark using any bookmark manager!'><img src='http://www.addme.com/images/button0-bm.gif' width='83' height='16' border='0' /></a>
	<!-- AddThis Bookmark Button END -->
	</p>
	<p>Facebook: <a href='http://www.facebook.com/sethgodin?_fb_noscript=1'>Seth's Facebook</a><br>
	Twitter: <a href='http://twitter.com/ThisIsSethsBlog'>@thisissethsblog</a></p>
	</div>
</div>
			
			
	<div class='module-archives module'>
		<h2 class='module-header'>Recent Posts</h2>
		<div class='module-content'>
			<ul class='module-list'>
				
					
					<li class='module-list-item'><a href='http://sethgodin.typepad.com/seths_blog/2016/05/striking-a-chord.html'>Striking a chord</a></li>
					
				
					
					<li class='module-list-item'><a href='http://sethgodin.typepad.com/seths_blog/2016/05/the-problem-you-cant-talk-about.html'>The problem you can't talk about</a></li>
					
				
					
					<li class='module-list-item'><a href='http://sethgodin.typepad.com/seths_blog/2016/05/avoid-being-treated-like-a-child.html'>On being treated like an adult</a></li>
					
				
					
					<li class='module-list-item'><a href='http://sethgodin.typepad.com/seths_blog/2016/05/rigor.html'>Rigor</a></li>
					
				
					
					<li class='module-list-item'><a href='http://sethgodin.typepad.com/seths_blog/2016/05/calling-your-finding.html'>Calling your finding</a></li>
					
				
					
					<li class='module-list-item'><a href='http://sethgodin.typepad.com/seths_blog/2016/05/unlimited-bowling.html'>Unlimited bowling</a></li>
					
				
					
					<li class='module-list-item'><a href='http://sethgodin.typepad.com/seths_blog/2016/05/the-most-common-b2b-objection.html'>The most common b2b objection (and the one we have about most innovations)</a></li>
					
				
					
					<li class='module-list-item'><a href='http://sethgodin.typepad.com/seths_blog/2016/05/what-do-i-owe-you.html'>'What do I owe you?'</a></li>
					
				
					
					<li class='module-list-item'><a href='http://sethgodin.typepad.com/seths_blog/2016/05/learning-from-the-rejection.html'>Learning from the rejection</a></li>
					
				
					
					<li class='module-list-item'><a href='http://sethgodin.typepad.com/seths_blog/2016/05/duck.html'>Duck!</a></li>
					
				
			</ul>
		</div>
	</div>

			
			<!-- monthly archives -->
<div class='module-archives module'>
<h2 class='module-header'><a href='http://sethgodin.typepad.com/seths_blog/archives.html'>Archives</a></h2>
<div class='module-content'>
									<ul class='module-list'>
			
			<li class='module-list-item'><a href='http://sethgodin.typepad.com/seths_blog/2016/05/index.html'>May 2016</a></li>
			
				
			<li class='module-list-item'><a href='http://sethgodin.typepad.com/seths_blog/2016/04/index.html'>April 2016</a></li>
			
				
			<li class='module-list-item'><a href='http://sethgodin.typepad.com/seths_blog/2016/03/index.html'>March 2016</a></li>
			
				
			<li class='module-list-item'><a href='http://sethgodin.typepad.com/seths_blog/2016/02/index.html'>February 2016</a></li>
			
				
			<li class='module-list-item'><a href='http://sethgodin.typepad.com/seths_blog/2016/01/index.html'>January 2016</a></li>
			
				
			<li class='module-list-item'><a href='http://sethgodin.typepad.com/seths_blog/2015/12/index.html'>December 2015</a></li>
			
				
			<li class='module-list-item'><a href='http://sethgodin.typepad.com/seths_blog/2015/11/index.html'>November 2015</a></li>
			
				
			<li class='module-list-item'><a href='http://sethgodin.typepad.com/seths_blog/2015/10/index.html'>October 2015</a></li>
			
				
			<li class='module-list-item'><a href='http://sethgodin.typepad.com/seths_blog/2015/09/index.html'>September 2015</a></li>
			
				
			<li class='module-list-item'><a href='http://sethgodin.typepad.com/seths_blog/2015/08/index.html'>August 2015</a></li>
								</ul>
							<p class='module-more'><a href='http://sethgodin.typepad.com/seths_blog/archives.html'>More...</a></p>
			
			
	
</div><!-- .module-content -->
</div><!-- .module-archives .module -->

			
			<div class='module-typelist module' style=' margin-top: 0px; padding-top: 0px; '>
	<h2 class='module-header' style='padding-bottom: 0; margin-bottom: 0'>Search</h2>
	<div class='typelist-plain module-content' style='padding-bottom: 0; margin-bottom: 0'>
	<!-- SiteSearch Google -->
	<form method=get action='http://www.google.com/search'>
	<input type=hidden name=ie value=UTF-8>
	<input type=hidden name=oe value=UTF-8>
	<input type='hidden' name='domains' value='sethgodin.typepad.com'>
	<p><a href='http://www.google.com/'><img src='http://www.google.com/intl/en/logos/Logo_25wht.gif' alt='Google'></a></p>
	<input type='text' name='q' size='20' maxlength='255' class='sidenav_input_text' value=''><br><input type='submit' name='btnG' value='search' class='sidenav_button'>
	<p style='font-family: verdana, arial, sans-serif; font-size: 10px; color: #727272;'><input type=radio name=sitesearch value=''>WWW <input type=radio name=sitesearch value='sethgodin.typepad.com' checked>SETH'S BLOG</p>
	</form>
	<!-- End SiteSearch Google -->
	<br />
	<a href='http://www.altmba.com'><img src='/sg/images/content/sidebar_altmba.jpg' alt='altmba' /></a><br />
	<br />
	</div>
</div>
			
			<!-- Seth's Web Pages TypeList -->
			<div id='seths_web_pages' class='module-typelist module'>
<h2 class='module-header'>Seth's Web Pages</h2>
	<div class='module-content'>
		<ul class='module-list'>
							<li class='module-list-item'><a title='' href='http://sethgodin.typepad.com/the_dip' >The Dip Blog</a></li>
							<li class='module-list-item'><a title='' href='http://sethgodin.com/sg/books.asp' >Books by Seth Godin</a></li>
							<li class='module-list-item'><a title='' href='http://sethgodin.typepad.com' >Seth's Main Blog</a></li>
							<li class='module-list-item'><a title='' href='http://www.squidoo.com/seth/' >Seth's Squidoo Lens</a></li>
							<li class='module-list-item'><a title='' href='http://www.allmarketersareliars.com' >All Marketers Are Liars Blog</a></li>
							<li class='module-list-item'><a title='Seth Godin: Author, Agent of Change' href='http://www.sethgodin.com' >SethGodin.com: Official Site</a></li>
			
		</ul>
	</div>
</div>

			
			<!-- Links TypeList -->
			<div id='links' class='module-typelist module'>
<h2 class='module-header'>Links</h2>
	<div class='module-content'>
		<ul class='module-list'>
							<li class='module-list-item'><a title='' href='http://www.sethgodin.com/sg/subscribe.asp' >Subscribe to Seth's Blog</a></li>
							<li class='module-list-item'><a title='An intensive, 4-week online workshop designed to accelerate leaders to become change agents for the future. Designed by Seth Godin, for you.' href='http://www.altmba.com' >altMBA</a></li>
							<li class='module-list-item'><a title='Designers of Seth's sites' href='http://www.onrampbranding.com' >onramp Branding</a></li>
			
		</ul>
	</div>
</div>
			

			<h2 class='module-header'>SETH'S BOOKS</h2>
<div class='module-content'>
<strong>Seth Godin has written 18 bestsellers that have been translated into 35 languages</strong><br>
<br>
<a href='http://sethgodin.com/sg/books.asp' style='text-decoration: none;'>The complete list of online retailers</a><br>
<hr>
<a href='http://squidoo.com/seth' style='text-decoration: none;'>Bonus stuff!</a><br>
<hr>
<a href='http://sethgodin.com/sg/books.asp' style='text-decoration: none;'>or click on a title below to see the list</a><br><br>
</div>
<!-- book sidebar -->
<div class='scrollable vertical_bookcycle'>
	<a class='vert_prev' id='vert_prev' href='#'><span>prev</span></a>
	<div id='vert_book_items'>
		<div class='book_item'><a href='#bl_the_icarus_deception' rel='facebox'><img class='vert_book_off hb_the_icarus_deception' onmouseover='this.className='vert_book_on hb_the_icarus_deception';' onmouseout='this.className='vert_book_off hb_the_icarus_deception';' src='/sg/images/home_book_nav/clear.spacer.png' width='130' height='102' id='the.icarus.deception' title='the.icarus.deception' border='0' /></a></div>
		<div class='book_item'><a href='#bl_whatcha_gonna_do_with_that_duck' rel='facebox'><img class='vert_book_off hb_whatcha_gonna_do_with_that_duck' onmouseover='this.className='vert_book_on hb_whatcha_gonna_do_with_that_duck';' onmouseout='this.className='vert_book_off hb_whatcha_gonna_do_with_that_duck';' src='/sg/images/home_book_nav/clear.spacer.png' width='130' height='102' id='whatcha.gonna.do.with.that.duck' title='whatcha.gonna.do.with.that.duck' border='0' /></a></div>
		<div class='book_item'><a href='#bl_v_is_for_vulnerable' rel='facebox'><img class='vert_book_off hb_v_is_for_vulnerable' onmouseover='this.className='vert_book_on hb_v_is_for_vulnerable';' onmouseout='this.className='vert_book_off hb_v_is_for_vulnerable';' src='/sg/images/home_book_nav/clear.spacer.png' width='130' height='102' id='v.is.for.vulnerable' title='v.is.for.vulnerable' border='0' /></a></div>
		<div class='book_item'><a href='#bl_we_are_all_weird' rel='facebox'><img class='vert_book_off hb_we_are_all_weird' onmouseover='this.className='vert_book_on hb_we_are_all_weird';' onmouseout='this.className='vert_book_off hb_we_are_all_weird';' src='/sg/images/home_book_nav/clear.spacer.png' width='130' height='102' id='we.are.all.weird' title='we.are.all.weird' border='0' /></a></div>
		<div class='book_item'><a href='#bl_linchpin' rel='facebox'><img class='vert_book_off hb_linchpin' onmouseover='this.className='vert_book_on hb_linchpin';' onmouseout='this.className='vert_book_off hb_linchpin';' src='/sg/images/home_book_nav/clear.spacer.png' width='130' height='102' id='linchpin' title='linchpin' border='0' /></a></div>
		<div class='book_item'><a href='#bl_alt_mba' rel='facebox'><img class='vert_book_off hb_alt_mba' onmouseover='this.className='vert_book_on hb_alt_mba';' onmouseout='this.className='vert_book_off hb_alt_mba';' src='/sg/images/home_book_nav/clear.spacer.png' width='130' height='102' id='alt.mba' title='alt.mba' border='0' /></a></div>
		<div class='book_item'><a href='#bl_poke_the_box' rel='facebox'><img class='vert_book_off hb_poke_the_box' onmouseover='this.className='vert_book_on hb_poke_the_box';' onmouseout='this.className='vert_book_off hb_poke_the_box';' src='/sg/images/home_book_nav/clear.spacer.png' width='130' height='102' id='poke.the.box' title='poke.the.box' border='0' /></a></div>
		<div class='book_item'><a href='#bl_meatball_sundae' rel='facebox'><img class='vert_book_off hb_meatball_sundae' onmouseover='this.className='vert_book_on hb_meatball_sundae';' onmouseout='this.className='vert_book_off hb_meatball_sundae';' src='/sg/images/home_book_nav/clear.spacer.png' width='130' height='102' id='meatball.sundae' title='meatball.sundae' border='0' /></a></div>
		<div class='book_item'><a href='#bl_the_dip' rel='facebox'><img class='vert_book_off hb_the_dip' onmouseover='this.className='vert_book_on hb_the_dip';' onmouseout='this.className='vert_book_off hb_the_dip';' src='/sg/images/home_book_nav/clear.spacer.png' width='130' height='102' id='the.dip' title='the.dip' border='0' /></a></div>
		<div class='book_item'><a href='#bl_tribes' rel='facebox'><img class='vert_book_off hb_tribes' onmouseover='this.className='vert_book_on hb_tribes';' onmouseout='this.className='vert_book_off hb_tribes';' src='/sg/images/home_book_nav/clear.spacer.png' width='130' height='102' id='tribes' title='tribes' border='0' /></a></div>
		<div class='book_item'><a href='#bl_free_prize_inside' rel='facebox'><img class='vert_book_off hb_free_prize_inside' onmouseover='this.className='vert_book_on hb_free_prize_inside';' onmouseout='this.className='vert_book_off hb_free_prize_inside';' src='/sg/images/home_book_nav/clear.spacer.png' width='130' height='102' id='free.prize.inside' title='free.prize.inside' border='0' /></a></div>
		<div class='book_item'><a href='#bl_permission_marketing' rel='facebox'><img class='vert_book_off hb_permission_marketing' onmouseover='this.className='vert_book_on hb_permission_marketing';' onmouseout='this.className='vert_book_off hb_permission_marketing';' src='/sg/images/home_book_nav/clear.spacer.png' width='130' height='102' id='permission.marketing' title='permission.marketing' border='0' /></a></div>
		<div class='book_item'><a href='#bl_unleashing_the_ideavirus' rel='facebox'><img class='vert_book_off hb_unleashing_the_ideavirus' onmouseover='this.className='vert_book_on hb_unleashing_the_ideavirus';' onmouseout='this.className='vert_book_off hb_unleashing_the_ideavirus';' src='/sg/images/home_book_nav/clear.spacer.png' width='130' height='102' id='unleashing.the.ideavirus' title='unleashing.the.ideavirus' border='0' /></a></div>
		<div class='book_item'><a href='#bl_small_is_the_new_big' rel='facebox'><img class='vert_book_off hb_small_is_the_new_big' onmouseover='this.className='vert_book_on hb_small_is_the_new_big';' onmouseout='this.className='vert_book_off hb_small_is_the_new_big';' src='/sg/images/home_book_nav/clear.spacer.png' width='130' height='102' id='small.is.the.new.big' title='small.is.the.new.big' border='0' /></a></div>
		<div class='book_item'><a href='#bl_survival_is_not_enough' rel='facebox'><img class='vert_book_off hb_survival_is_not_enough' onmouseover='this.className='vert_book_on hb_survival_is_not_enough';' onmouseout='this.className='vert_book_off hb_survival_is_not_enough';' src='/sg/images/home_book_nav/clear.spacer.png' width='130' height='102' id='survival.is.not.enough' title='survival.is.not.enough' border='0' /></a></div>
		<div class='book_item'><a href='#bl_purple_cow' rel='facebox'><img class='vert_book_off hb_purple_cow' onmouseover='this.className='vert_book_on hb_purple_cow';' onmouseout='this.className='vert_book_off hb_purple_cow';' src='/sg/images/home_book_nav/clear.spacer.png' width='130' height='102' id='purple.cow' title='purple.cow' border='0' /></a></div>
		<div class='book_item'><a href='#bl_all_marketers_tell_stories' rel='facebox'><img class='vert_book_off hb_all_marketers_tell_stories' onmouseover='this.className='vert_book_on hb_all_marketers_tell_stories';' onmouseout='this.className='vert_book_off hb_all_marketers_tell_stories';' src='/sg/images/home_book_nav/clear.spacer.png' width='130' height='102' id='all.marketers.tell.stories' title='all.marketers.tell.stories' border='0' /></a></div>
		<div class='book_item'><a href='#bl_the_big_moo' rel='facebox'><img class='vert_book_off hb_the_big_moo' onmouseover='this.className='vert_book_on hb_the_big_moo';' onmouseout='this.className='vert_book_off hb_the_big_moo';' src='/sg/images/home_book_nav/clear.spacer.png' width='130' height='102' id='the.big.moo' title='the.big.moo' border='0' /></a></div>
		<div class='book_item'><a href='#bl_the_big_red_fez' rel='facebox'><img class='vert_book_off hb_the_big_red_fez' onmouseover='this.className='vert_book_on hb_the_big_red_fez';' onmouseout='this.className='vert_book_off hb_the_big_red_fez';' src='/sg/images/home_book_nav/clear.spacer.png' width='130' height='102' id='big.red.fez' title='big.red.fez' border='0' /></a></div>
	</div>
	<a class='vert_next' id='vert_next' href='#'><span>next</span></a>
</div>

<!--- book layers --->

<div id='bl_alt_mba' class='fb_window'>
	<div class='book_img_info'>
		<div class='book_img'><img src='/sg/images/book_layers/alt.mba.png' width='434' height='371' alt='alt.mba' border='0' /></div>
		<div class='book_info'>
		<div class='return_to_page'><a href='#bl_alt_mba' onclick='javascript:jQuery(document).trigger('close.facebox');'>x</a></div>
		<div class='book_copy'>
		<h3>altMBA</h3>
		<p>An intensive, 4-week online workshop designed to accelerate leaders to become change agents for the future. Designed by Seth Godin, for you.</p>
		<h4>ONLINE:</h4>
	    <ul class='booksitelist'>
			<li><a href='http://www.altmba.com/' title='altMBA' target='_parent'>altMBA</a></li>
	    </ul>
		</div>
		</div>
	</div>
</div>

<div id='bl_all_marketers_tell_stories' class='fb_window'>
	<div class='book_img_info'>
		<div class='book_img'><img src='/sg/images/book_layers/all.marketers.tell.stories.png' width='434' height='371' alt='all.marketers.tell.stories' border='0' /></div>
		<div class='book_info'>
		<div class='return_to_page'><a href='#bl_all_marketers_tell_storiess' onclick='javascript:jQuery(document).trigger('close.facebox');'>x</a></div>
		<div class='book_copy'>
		<h3>All Marketers Tell Stories</h3>
		<p>Seth's most important book about the art of marketing</p>
		<h4>ONLINE:</h4>
	    <ul class='booksitelist'>
			<li><a href='http://www.amazon.com/exec/obidos/ASIN/1591841003/permissionmarket' title='amazon' target='_parent'>amazon</a></li>
			<li><a href='http://search.barnesandnoble.com/booksearch/isbnInquiry.asp?z=y&EAN=9781591841005' title='Barnes and Noble' target='_parent'>Barnes &amp; Noble</a></li>
			<li><a href='http://800ceoread.com/products/?ISBN=9781591841005' title='CEO READ' target='_parent'>CEO READ</a>&nbsp;&nbsp;&nbsp;(Bulk Orders)</li>
	    </ul>
	    <h4>IN STORES:</h4>
	    <ul class='booksitelist'>
			<li><a href='http://www.indiebound.org/book/9781591841005' title='IndieBound.org' target='_parent'>Indie Bookstores</a></li>
			<li><a href='international_links.asp'>International Links</a></li>
		</ul>
		</div>
		</div>
	</div>
</div>

<div id='bl_free_prize_inside' class='fb_window'>
	<div class='book_img_info'>
		<div class='book_img'><img src='/sg/images/book_layers/free.prize.inside.png' width='434' height='371' alt='free.prize.inside' border='0' /></div>
		<div class='book_info'>
		<div class='return_to_page'><a href='#bl_free_prize_inside' onclick='javascript:jQuery(document).trigger('close.facebox');'>x</a></div>
		<div class='book_copy'>
		<h3>Free Prize Inside</h3>
		<p>The practical sequel to Purple Cow</p>
		<h4>ONLINE:</h4>
	    <ul class='booksitelist'>
			<li><a href='http://www.amazon.com/exec/obidos/ASIN/1591840414/permissionmarket' title='amazon' target='_parent'>amazon</a></li>
			<li><a href='http://search.barnesandnoble.com/booksearch/isbnInquiry.asp?z=y&EAN=9781591840411' title='Barnes and Noble' target='_parent'>Barnes &amp; Noble</a></li>
			<li><a href='http://www.bookdepository.com/Free-Prize-Inside-Seth-Godin/9780141019710/?a_aid=sethgodin'>The Book Depository</a>&nbsp;&nbsp;&nbsp;(Free Worldwide Delivery)</li>
			<li><a href='http://800ceoread.com/products/?ISBN=9781591841678' title='CEO READ' target='_parent'>CEO READ</a>&nbsp;&nbsp;&nbsp;(Bulk Orders)</li>
	    </ul>
	    <h4>IN STORES:</h4>
	    <ul class='booksitelist'>
			<li><a href='http://www.indiebound.org/book/9781591841678' title='IndieBound.org' target='_parent'>Indie Bookstores</a></li>
			<li><a href='international_links.asp'>International Links</a></li>
		</ul>
		</div>
		</div>
	</div>
</div>

<div id='bl_linchpin' class='fb_window'>
	<div class='book_img_info'>
		<div class='book_img'><img src='/sg/images/book_layers/linchpin.png' width='434' height='371' alt='linchpin' border='0' /></div>
		<div class='book_info'>
		<div class='return_to_page'><a href='#bl_linchpin' onclick='javascript:jQuery(document).trigger('close.facebox');'>x</a></div>
		<div class='book_copy'>
		<h3>Linchpin</h3>
		<p>An instant bestseller, the book that brings all of Seth's ideas together.</p>
		<h4>ONLINE:</h4>
		<ul class='booksitelist'>
			<li><a href='http://www.amazon.com/exec/obidos/ASIN/1591843162/permissionmarket' title='amazon' target='_parent'>amazon</a></li>
			<li><a href='http://search.barnesandnoble.com/Linchpin/Seth-Godin/e/9781591843160' title='Barnes and Noble' target='_parent'>Barnes &amp; Noble</a></li>
			<li><a href='http://www.bookdepository.com/Linchpin-Seth-Godin/9780749953355/?a_aid=sethgodin'>The Book Depository</a>&nbsp;&nbsp;&nbsp;(Free Worldwide Delivery)</li>
			<li><a href='http://800ceoread.com/book/show/9781591843160' title='CEO READ' target='_parent'>CEO READ</a>&nbsp;&nbsp;&nbsp;(Bulk Orders)</li>
	    </ul>
	    <h4>IN STORES:</h4>
	    <ul class='booksitelist'>
			<li><a href='http://www.indiebound.org/book/9781591843160' title='IndieBound.org' target='_parent'>Indie Bookstores</a></li>
			<li><a href='international_links.asp'>International Links</a></li>
		</ul>
		</div>
		</div>
	</div>
</div>

<div id='bl_meatball_sundae' class='fb_window'>
	<div class='book_img_info'>
		<div class='book_img'><img src='/sg/images/book_layers/meatball.sundae.png' width='434' height='371' alt='meatball.sundae' border='0' /></div>
		<div class='book_info'>
		<div class='return_to_page'><a href='#bl_meatball_sundae' onclick='javascript:jQuery(document).trigger('close.facebox');'>x</a></div>
		<div class='book_copy'>
		<h3>Meatball Sundae</h3>
		<p>Why the internet works (and doesn't) for your business. And vice versa.</p>
		<h4>ONLINE:</h4>
	    <ul class='booksitelist'>
			<li><a href='http://www.amazon.com/exec/obidos/ASIN/1591841747/permissionmarket' title='amazon' target='_parent'>amazon</a></li>
			<li><a href='http://search.barnesandnoble.com/Meatball-Sundae/Seth-Godin/e/9781591841746/?itm=1&USRI=meatball+sundae+is+your+marketing+out+of+sync' title='Barnes and Noble' target='_parent'>Barnes &amp; Noble</a></li>
			<li><a href='http://www.bookdepository.com/Meatball-Sundae-Seth-Godin/9781591841746/?a_aid=sethgodin'>The Book Depository</a>&nbsp;&nbsp;&nbsp;(Free Worldwide Delivery)</li>
			<li><a href='http://800ceoread.com/book/show/9781591841746-Meatball_Sundae' title='CEO READ' target='_parent'>CEO READ</a>&nbsp;&nbsp;&nbsp;(Bulk Orders)</li>
	    </ul>
	    <h4>IN STORES:</h4>
	    <ul class='booksitelist'>
			<li><a href='http://www.indiebound.org/book/9781591841746' title='IndieBound.org' target='_parent'>Indie Bookstores</a></li>
			<li><a href='international_links.asp'>International Links</a></li>
		</ul>
		</div>
		</div>
	</div>
</div>

<div id='bl_permission_marketing' class='fb_window'>
	<div class='book_img_info'>
		<div class='book_img'><img src='/sg/images/book_layers/permission.marketing.png' width='434' height='371' alt='permission.marketing' border='0' /></div>
		<div class='book_info'>
		<div class='return_to_page'><a href='#bl_permission_marketing' onclick='javascript:jQuery(document).trigger('close.facebox');'>x</a></div>
		<div class='book_copy'>
		<h3>Permission Marketing</h3>
		<p>The classic Named 'Best Business Book' by Fortune.</p>
		<h4>ONLINE:</h4>
	    <ul class='booksitelist'>
			<li><a href='http://www.amazon.com/exec/obidos/ASIN/0684856360/permissionmarket' title='amazon' target='_parent'>amazon</a></li>
			<li><a href='http://search.barnesandnoble.com/booksearch/isbnInquiry.asp?z=y&EAN=9780684856360' title='Barnes and Noble' target='_parent'>Barnes &amp; Noble</a></li>
			<li><a href='http://www.bookdepository.com/Permission-Marketing-Seth-Godin/9780684856360/?a_aid=sethgodin'>The Book Depository</a>&nbsp;&nbsp;&nbsp;(Free Worldwide Delivery)</li>
			<li><a href='http://800ceoread.com/products/?ISBN=9780684856360' title='CEO READ' target='_parent'>CEO READ</a>&nbsp;&nbsp;&nbsp;(Bulk Orders)</li>
	    </ul>
	    <h4>IN STORES:</h4>
	    <ul class='booksitelist'>
			<li><a href='http://www.indiebound.org/book/9780684856360' title='IndieBound.org' target='_parent'>Indie Bookstores</a></li>
			<li><a href='international_links.asp'>International Links</a></li>
		</ul>
		</div>
		</div>
	</div>
</div>

<div id='bl_poke_the_box' class='fb_window'>
	<div class='book_img_info'>
		<div class='book_img'><img src='/sg/images/book_layers/poke.the.box.png' width='434' height='371' alt='poke.the.box' border='0' /></div>
		<div class='book_info'>
		<div class='return_to_page'><a href='#bl_poke_the_box' onclick='javascript:jQuery(document).trigger('close.facebox');'>x</a></div>
		<div class='book_copy'>
		<h3>Poke The Box</h3>
		<p>The latest book, Poke The Box is a call to action about the initiative you're taking - in your job or in your life, and Seth once again breaks the traditional publishing model by releasing it through <a href='http://www.thedominoproject.com'>The Domino Project</a>.</p>
		<h4>ONLINE:</h4>
		<ul class='booksitelist'>
			<li><a href='http://www.amazon.com/exec/obidos/ASIN/1936719002/permissionmarket' title='amazon' target='_parent'>amazon</a></li>
			<li><a href='http://search.barnesandnoble.com/Poke-the-Box/Seth-Godin/e/9781936719006' title='Barnes and Noble' target='_parent'>Barnes &amp; Noble</a></li>
			<li><a href='http://www.bookdepository.com/Poke-Box-Seth-Godin/9781936719006/?a_aid=sethgodin'>The Book Depository</a>&nbsp;&nbsp;&nbsp;(Free Worldwide Delivery)</li>
			<li><a href='http://800ceoread.com/book/show/9781936719006-Poke_the_Box' title='CEO READ' target='_parent'>CEO READ</a>&nbsp;&nbsp;&nbsp;(Bulk Orders)</li>
	    </ul>
	    <h4>IN STORES:</h4>
	    <ul class='booksitelist'>
			<li><a href='http://www.indiebound.org/book/9781936719006' title='IndieBound.org' target='_parent'>Indie Bookstores</a></li>
			<li><a href='international_links.asp'>International Links</a></li>
		</ul>
		</div>
		</div>
	</div>
</div>

<div id='bl_purple_cow' class='fb_window'>
	<div class='book_img_info'>
		<div class='book_img'><img src='/sg/images/book_layers/purple.cow.png' width='434' height='371' alt='purple.cow' border='0' /></div>
		<div class='book_info'>
		<div class='return_to_page'><a href='#bl_purple_cow' onclick='javascript:jQuery(document).trigger('close.facebox');'>x</a></div>
		<div class='book_copy'>
		<h3>Purple Cow</h3>
		<p>The worldwide bestseller. Essential reading about remarkable products and services.</p>
		<h4>ONLINE:</h4>
	    <ul class='booksitelist'>
			<li><a href='http://www.amazon.com/exec/obidos/ASIN/1591843170/permissionmarket' title='amazon' target='_parent'>amazon</a></li>
			<li><a href='http://search.barnesandnoble.com/Purple-Cow/Seth-Godin/e/9781591843177/?itm=1&USRI=purple+cow' title='Barnes and Noble' target='_parent'>Barnes &amp; Noble</a></li>
			<li><a href='http://www.bookdepository.com/Purple-Cow-Seth-Godin/9781591843177/?a_aid=sethgodin'>The Book Depository</a>&nbsp;&nbsp;&nbsp;(Free Worldwide Delivery)</li>
			<li><a href='http://800ceoread.com/products/?ISBN=9781591840213' title='CEO READ' target='_parent'>CEO READ</a>&nbsp;&nbsp;&nbsp;(Bulk Orders)</li>
	    </ul>
	    <h4>IN STORES:</h4>
	    <ul class='booksitelist'>
			<li><a href='http://www.indiebound.org/book/9781591840213' title='IndieBound.org' target='_parent'>Indie Bookstores</a></li>
			<li><a href='international_links.asp'>International Links</a></li>
		</ul>
		</div>
		</div>
	</div>
</div>

<div id='bl_small_is_the_new_big' class='fb_window'>
	<div class='book_img_info'>
		<div class='book_img'><img src='/sg/images/book_layers/small.is.the.new.big.png' width='434' height='371' alt='small.is.the.new.big' border='0' /></div>
		<div class='book_info'>
		<div class='return_to_page'><a href='#bl_small_is_the_new_big' onclick='javascript:jQuery(document).trigger('close.facebox');'>x</a></div>
		<div class='book_copy'>
		<h3>Small is the New Big</h3>
		<p>A long book filled with short pieces from Fast Company and the blog. Guaranteed to make you think.</p>
		<h4>ONLINE:</h4>
	    <ul class='booksitelist'>
			<li><a href='http://www.amazon.com/exec/obidos/ASIN/1591841267/permissionmarket' title='amazon' target='_parent'>amazon</a></li>
			<li><a href='http://search.barnesandnoble.com/booksearch/isbnInquiry.asp?z=y&EAN=9781591841265' title='Barnes and Noble' target='_parent'>Barnes &amp; Noble</a></li>
			<li><a href='http://www.bookdepository.com/Small-is-New-Big-Seth-Godin/9780141030531/?a_aid=sethgodin'>The Book Depository</a>&nbsp;&nbsp;&nbsp;(Free Worldwide Delivery)</li>
			<li><a href='http://800ceoread.com/products/?ISBN=9781591841265' title='CEO READ' target='_parent'>CEO READ</a>&nbsp;&nbsp;&nbsp;(Bulk Orders)</li>
	    </ul>
	    <h4>IN STORES:</h4>
	    <ul class='booksitelist'>
			<li><a href='http://www.indiebound.org/book/9781591841265' title='IndieBound.org' target='_parent'>Indie Bookstores</a></li>
			<li><a href='international_links.asp'>International Links</a></li>
		</ul>
		</div>
		</div>
	</div>
</div>

<div id='bl_survival_is_not_enough' class='fb_window'>
	<div class='book_img_info'>
		<div class='book_img'><img src='/sg/images/book_layers/survival.is.not.enough.png' width='434' height='371' alt='survival.is.not.enough' border='0' /></div>
		<div class='book_info'>
		<div class='return_to_page'><a href='#bl_survival_is_not_enough' onclick='javascript:jQuery(document).trigger('close.facebox');'>x</a></div>
		<div class='book_copy'>
		<h3>Survival is Not Enough</h3>
		<p>Seth's worst seller and personal favorite. Change. How it works (and doesn't).</p>
		<h4>ONLINE:</h4>
	    <ul class='booksitelist'>
			<li><a href='http://www.amazon.com/exec/obidos/ASIN/0743233387/permissionmarket' title='amazon' target='_parent'>amazon</a></li>
			<li><a href='http://search.barnesandnoble.com/booksearch/isbnInquiry.asp?z=y&EAN=9780743233385' title='Barnes and Noble' target='_parent'>Barnes &amp; Noble</a></li>
			<li><a href='http://www.bookdepository.com/Survival-is-Not-Enough-Us-Editio-Godin/9780743233385/?a_aid=sethgodin'>The Book Depository</a>&nbsp;&nbsp;&nbsp;(Free Worldwide Delivery)</li>
			<li><a href='http://800ceoread.com/products/?ISBN=0743225716' title='CEO READ' target='_parent'>CEO READ</a>&nbsp;&nbsp;&nbsp;(Bulk Orders)</li>
	    </ul>
	    <h4>IN STORES:</h4>
	    <ul class='booksitelist'>
			<li><a href='http://www.indiebound.org/book/9780743233385' title='IndieBound.org' target='_parent'>Indie Bookstores</a></li>
			<li><a href='international_links.asp'>International Links</a></li>
		</ul>
		</div>
		</div>
	</div>
</div>

<div id='bl_the_big_moo' class='fb_window'>
	<div class='book_img_info'>
		<div class='book_img'><img src='/sg/images/book_layers/the.big.moo.png' width='434' height='371' alt='the.big.moo' border='0' /></div>
		<div class='book_info'>
		<div class='return_to_page'><a href='#bl_the_big_moo' onclick='javascript:jQuery(document).trigger('close.facebox');'>x</a></div>
		<div class='book_copy'>
		<h3>The Big Moo</h3>
		<p>All for charity. Includes original work from Malcolm Gladwell, Tom Peters and Promise Phelon.</p>
		<h4>ONLINE:</h4>
	    <ul class='booksitelist'>
			<li><a href='http://www.amazon.com/exec/obidos/ASIN/1591841038/permissionmarket' title='amazon' target='_parent'>amazon</a></li>
			<li><a href='http://search.barnesandnoble.com/booksearch/isbnInquiry.asp?z=y&EAN=9781591841036' title='Barnes and Noble' target='_parent'>Barnes &amp; Noble</a></li>
			<li><a href='http://800ceoread.com/products/?ISBN=9781591841036' title='CEO READ' target='_parent'>CEO READ</a>&nbsp;&nbsp;&nbsp;(Bulk Orders)</li>
	    </ul>
	    <h4>IN STORES:</h4>
	    <ul class='booksitelist'>
			<li><a href='http://www.indiebound.org/book/9781591841036' title='IndieBound.org' target='_parent'>Indie Bookstores</a></li>
			<li><a href='international_links.asp'>International Links</a></li>
		</ul>
		</div>
		</div>
	</div>
</div>

<div id='bl_the_big_red_fez' class='fb_window'>
	<div class='book_img_info'>
		<div class='book_img'><img src='/sg/images/book_layers/the.big.red.fez.png' width='434' height='371' alt='the.big.red.fez' border='0' /></div>
		<div class='book_info'>
		<div class='return_to_page'><a href='#bl_the_big_red_fez' onclick='javascript:jQuery(document).trigger('close.facebox');'>x</a></div>
		<div class='book_copy'>
		<h3>The Big Red Fez</h3>
		<p>Top 5 Amazon ebestseller for a year. All about web sites that work.</p>
		<h4>ONLINE:</h4>
	    <ul class='booksitelist'>
			<li><a href='http://www.amazon.com/exec/obidos/ASIN/0743227905/permissionmarket' title='amazon' target='_parent'>amazon</a></li>
			<li><a href='http://search.barnesandnoble.com/booksearch/isbnInquiry.asp?z=y&EAN=9780743227902' title='Barnes and Noble' target='_parent'>Barnes &amp; Noble</a></li>
			<li><a href='http://www.bookdepository.com/Big-Red-Fez-Seth-Godin/9780743227902/?a_aid=sethgodin'>The Book Depository</a>&nbsp;&nbsp;&nbsp;(Free Worldwide Delivery)</li>
			<li><a href='http://800ceoread.com/products/?ISBN=9780743227902' title='CEO READ' target='_parent'>CEO READ</a>&nbsp;&nbsp;&nbsp;(Bulk Orders)</li>
	    </ul>
	    <h4>IN STORES:</h4>
	    <ul class='booksitelist'>
			<li><a href='http://www.indiebound.org/book/9780743227902' title='IndieBound.org' target='_parent'>Indie Bookstores</a></li>
			<li><a href='international_links.asp'>International Links</a></li>
		</ul>
		</div>
		</div>
	</div>
</div>

<div id='bl_the_dip' class='fb_window'>
	<div class='book_img_info'>
		<div class='book_img'><img src='/sg/images/book_layers/the.dip.png' width='434' height='371' alt='the.dip' border='0' /></div>
		<div class='book_info'>
		<div class='return_to_page'><a href='#bl_the_dip' onclick='javascript:jQuery(document).trigger('close.facebox');'>x</a></div>
		<div class='book_copy'>
		<h3>The Dip</h3>
		<p>A short book about quitting and being the best in the world. It's about life, not just marketing.</p>
		<h4>ONLINE:</h4>
	    <ul class='booksitelist'>
			<li><a href='http://www.amazon.com/exec/obidos/ASIN/1591841666/permissionmarket' title='amazon' target='_parent'>amazon</a></li>
			<li><a href='http://search.barnesandnoble.com/booksearch/isbnInquiry.asp?z=y&EAN=9781591841661' title='Barnes and Noble' target='_parent'>Barnes &amp; Noble</a></li>
			<li><a href='http://www.bookdepository.com/Dip-Seth-Godin/9781591841661/?a_aid=sethgodin'>The Book Depository</a>&nbsp;&nbsp;&nbsp;(Free Worldwide Delivery)</li>
			<li><a href='http://800ceoread.com/book/show/9781591841661-The_Dip' title='CEO READ' target='_parent'>CEO READ</a>&nbsp;&nbsp;&nbsp;(Bulk Orders)</li>
	    </ul>
	    <h4>IN STORES:</h4>
	    <ul class='booksitelist'>
			<li><a href='http://www.indiebound.org/book/9781591841661' title='IndieBound.org' target='_parent'>Indie Bookstores</a></li>
			<li><a href='international_links.asp'>International Links</a></li>
		</ul>
		</div>
		</div>
	</div>
</div>

<div id='bl_the_icarus_deception' class='fb_window'>
	<div class='book_img_info'>
		<div class='book_img'><img src='/sg/images/book_layers/the.icarus.deception.png' width='434' height='371' alt='the.icarus.deception' border='0' /></div>
		<div class='book_info'>
		<div class='return_to_page'><a href='#bl_the_icarus_deception' onclick='javascript:jQuery(document).trigger('close.facebox');'>x</a></div>
		<div class='book_copy'>
		<h3>The Icarus Deception</h3>
		<p>Seth's most personal book, a look at the end of the industrial economy and what happens next.</p>
		<h4>ONLINE:</h4>
		<ul class='booksitelist'>
			<li><a href='http://www.amazon.com/exec/obidos/ASIN/1591846072/permissionmarket' title='amazon' target='_parent'>amazon</a></li>
			<li><a href='http://www.barnesandnoble.com/w/the-icarus-deception-seth-godin/1112575504' title='Barnes and Noble' target='_parent'>Barnes &amp; Noble</a></li>
			<li><a href='http://www.bookdepository.com/Icarus-Deception-Seth-Godin/9781591846079/?a_aid=sethgodin'>The Book Depository</a>&nbsp;&nbsp;&nbsp;(Free Worldwide Delivery)</li>
			<li><a href='http://800ceoread.com/book/show/9781591846079-Icarus_Deception' title='CEO READ' target='_parent'>CEO READ</a>&nbsp;&nbsp;&nbsp;(Bulk Orders)</li>
	    </ul>
	    <h4>IN STORES:</h4>
	    <ul class='booksitelist'>
			<li><a href='http://www.indiebound.org/book/9781591846079' title='IndieBound.org' target='_parent'>Indie Bookstores</a></li>
			<li><a href='http://www.sethgodin.com/sg/international_links.asp'>International Links</a></li>
		</ul>
		</div>
		</div>
	</div>
</div>

<div id='bl_tribes' class='fb_window'>
	<div class='book_img_info'>
		<div class='book_img'><img src='/sg/images/book_layers/tribes.png' width='434' height='371' alt='tribes' border='0' /></div>
		<div class='book_info'>
		<div class='return_to_page'><a href='#bl_tribes' onclick='javascript:jQuery(document).trigger('close.facebox');'>x</a></div>
		<div class='book_copy'>
		<h3>Tribes</h3>
		<p>'Book of the year,' a perennial bestseller about leading, connecting and creating movements.</p>
		<h4>ONLINE:</h4>
	    <ul class='booksitelist'>
			<li><a href='http://www.amazon.com/exec/obidos/ASIN/1591842336/permissionmarket' title='amazon' target='_parent'>amazon</a></li>
			<li><a href='http://www.barnesandnoble.com/w/tribes-seth-godin/1101956589?ean=9781591842330' title='Barnes and Noble' target='_parent'>Barnes &amp; Noble</a></li>
			<li><a href='http://www.bookdepository.com/Tribes-Seth-Godin/9781591842330/?a_aid=sethgodin'>The Book Depository</a>&nbsp;&nbsp;&nbsp;(Free Worldwide Delivery)</li>
			<li><a href='http://800ceoread.com/book/show/9781591842330-Tribes' title='CEO READ' target='_parent'>CEO READ</a>&nbsp;&nbsp;&nbsp;(Bulk Orders)</li>
	    </ul>
	    <h4>IN STORES:</h4>
	    <ul class='booksitelist'>
			<li><a href='http://www.indiebound.org/book/9781591842330' title='IndieBound.org' target='_parent'>Indie Bookstores</a></li>
			<li><a href='international_links.asp'>International Links</a></li>
		</ul>
		</div>
		</div>
	</div>
</div>

<div id='bl_unleashing_the_ideavirus' class='fb_window'>
	<div class='book_img_info'>
		<div class='book_img'><img src='/sg/images/book_layers/unleashing.the.ideavirus.png' width='434' height='371' alt='unleashing.the.ideavirus' border='0' /></div>
		<div class='book_info'>
		<div class='return_to_page'><a href='#bl_unleashing_the_ideavirus' onclick='javascript:jQuery(document).trigger('close.facebox');'>x</a></div>
		<div class='book_copy'>
		<h3>Unleashing the Ideavirus</h3>
		<p>More than 3,000,000 copies downloaded, perhaps the most important book to read about creating ideas that spread.</p>
		<h4>ONLINE:</h4>
	    <ul class='booksitelist'>
			<li><a href='http://www.amazon.com/exec/obidos/ASIN/0786887176/permissionmarket' title='amazon' target='_parent'>amazon</a></li>
			<li><a href='http://search.barnesandnoble.com/booksearch/isbnInquiry.asp?z=y&EAN=9780786887170' title='Barnes and Noble' target='_parent'>Barnes &amp; Noble</a></li>
			<li><a href='http://www.bookdepository.com/Unleashing-Ideavirus-Seth-Godin/9780786887170/?a_aid=sethgodin'>The Book Depository</a>&nbsp;&nbsp;&nbsp;(Free Worldwide Delivery)</li>
			<li><a href='http://800ceoread.com/products/?ISBN=9780786887170' title='CEO READ' target='_parent'>CEO READ</a>&nbsp;&nbsp;&nbsp;(Bulk Orders)</li>
	    </ul>
	    <h4>IN STORES:</h4>
	    <ul class='booksitelist'>
			<li><a href='http://www.indiebound.org/book/9780786887170' title='IndieBound.org' target='_parent'>Indie Bookstores</a></li>
			<li><a href='international_links.asp'>International Links</a></li>
		</ul>
		</div>
		</div>
	</div>
</div>

<div id='bl_v_is_for_vulnerable' class='fb_window'>
	<div class='book_img_info'>
		<div class='book_img'><img src='/sg/images/book_layers/v.is.for.vulnerable.png' width='434' height='371' alt='v.is.for.vulnerable' border='0' /></div>
		<div class='book_info'>
		<div class='return_to_page'><a href='#bl_v_is_for_vulnerable' onclick='javascript:jQuery(document).trigger('close.facebox');'>x</a></div>
		<div class='book_copy'>
		<h3>V Is For Vulnerable</h3>
		<p>A short, illustrated, kids-like book that takes the last chapter of Icarus and turns it into something worth sharing.</p>
		<h4>ONLINE:</h4>
		<ul class='booksitelist'>
			<li><a href='http://www.amazon.com/exec/obidos/ASIN/1591846102/permissionmarket' title='amazon' target='_parent'>amazon</a></li>
			<li><a href='http://www.barnesandnoble.com/w/v-is-for-vulnerable-seth-godin/1113892453' title='Barnes and Noble' target='_parent'>Barnes &amp; Noble</a></li>
			<li><a href='http://www.bookdepository.com/V-is-for-Vulnerable-Seth-Godin/9780670923045/?a_aid=sethgodin'>The Book Depository</a>&nbsp;&nbsp;&nbsp;(Free Worldwide Delivery)</li>
			<li><a href='http://800ceoread.com/book/show/9781591846109-V_Is_for_Vulnerable' title='CEO READ' target='_parent'>CEO READ</a>&nbsp;&nbsp;&nbsp;(Bulk Orders)</li>
	    </ul>
	    <h4>IN STORES:</h4>
	    <ul class='booksitelist'>
			<li><a href='http://www.indiebound.org/book/9781591846109' title='IndieBound.org' target='_parent'>Indie Bookstores</a></li>
			<li><a href='http://www.sethgodin.com/sg/international_links.asp'>International Links</a></li>
		</ul>
		</div>
		</div>
	</div>
</div>

<div id='bl_we_are_all_weird' class='fb_window'>
	<div class='book_img_info'>
		<div class='book_img'><img src='/sg/images/book_layers/we.are.all.weird.png' width='434' height='371' alt='we.are.all.weird' border='0' /></div>
		<div class='book_info'>
		<div class='return_to_page'><a href='#bl_we_are_all_weird' onclick='javascript:jQuery(document).trigger('close.facebox');'>x</a></div>
		<div class='book_copy'>
		<h3>We Are All Weird</h3>
		<p>The end of mass and how you can succeed by delighting a niche.</p>
		<h4>ONLINE:</h4>
		<ul class='booksitelist'>
			<li><a href='http://www.amazon.com/exec/obidos/ASIN/1936719223/permissionmarket' title='amazon' target='_parent'>amazon</a></li>
			<li><a href='http://www.barnesandnoble.com/w/we-are-all-weird-seth-godin/1110796060' title='Barnes and Noble' target='_parent'>Barnes &amp; Noble</a></li>
			<li><a href='http://www.bookdepository.com/We-Are-All-Weird-Seth-Godin/9781936719228/?a_aid=sethgodin'>The Book Depository</a>&nbsp;&nbsp;&nbsp;(Free Worldwide Delivery)</li>
			<li><a href='http://800ceoread.com/book/show/9781936719228-We_Are_All_Weird' title='CEO READ' target='_parent'>CEO READ</a>&nbsp;&nbsp;&nbsp;(Bulk Orders)</li>
	    </ul>
	    <h4>IN STORES:</h4>
	    <ul class='booksitelist'>
			<li><a href='http://www.indiebound.org/book/9781936719228' title='IndieBound.org' target='_parent'>Indie Bookstores</a></li>
			<li><a href='http://www.sethgodin.com/sg/international_links.asp'>International Links</a></li>
		</ul>
		</div>
		</div>
	</div>
</div>

<div id='bl_whatcha_gonna_do_with_that_duck' class='fb_window'>
	<div class='book_img_info'>
		<div class='book_img'><img src='/sg/images/book_layers/whatcha.gonna.do.with.that.duck.png' width='434' height='371' alt='whatcha.gonna.do.with.that.duck' border='0' /></div>
		<div class='book_info'>
		<div class='return_to_page'><a href='#bl_whatcha_gonna_do_with_that_duck' onclick='javascript:jQuery(document).trigger('close.facebox');'>x</a></div>
		<div class='book_copy'>
		<h3>Whatcha Gonna Do With That Duck?</h3>
		<p>The sequel to Small is the New Big. More than 600 pages of the best of Seth's blog.</p>
		<h4>ONLINE:</h4>
		<ul class='booksitelist'>
			<li><a href='http://www.amazon.com/exec/obidos/ASIN/1591846099/permissionmarket' title='amazon' target='_parent'>amazon</a></li>
			<li><a href='http://www.barnesandnoble.com/w/whatcha-gonna-do-with-that-duck-seth-godin/1112575498' title='Barnes and Noble' target='_parent'>Barnes &amp; Noble</a></li>
			<li><a href='http://www.bookdepository.co.uk/Whatcha-Gonna-Do-with-That-Duck-Seth-Godin/9780670922994/?a_aid=sethgodin'>The Book Depository</a>&nbsp;&nbsp;&nbsp;(Free Worldwide Delivery)</li>
			<li><a href='http://800ceoread.com/book/show/9781591846093-Whatcha_Gonna_Do_with_That_Duck_' title='CEO READ' target='_parent'>CEO READ</a>&nbsp;&nbsp;&nbsp;(Bulk Orders)</li>
	    </ul>
	    <h4>IN STORES:</h4>
	    <ul class='booksitelist'>
			<li><a href='http://www.indiebound.org/book/9781591846093' title='IndieBound.org' target='_parent'>Indie Bookstores</a></li>
			<li><a href='http://www.sethgodin.com/sg/international_links.asp'>International Links</a></li>
		</ul>
		</div>
		</div>
	</div>
</div>
<!--- end book layers --->
<br />
<!-- fire plugin onDocumentReady -->
<script type='text/javascript' language='javascript'>
$(function() {	
	//	Scrolled by user interaction
	$('#vert_book_items').carouFredSel({
		circular: true,
		mousewheel: true,
		infinite: true,
		items 		: 7,
		direction	: 'up',  
		auto : {  
			duration    : 3000,  
			timeoutDuration: 15000,  
			pauseOnHover: true 
		},
		prev: '#vert_prev',
		next: '#vert_next',
		scroll : 5

}).find('.book_item').hover(
	function() { $(this).find('div').slideDown(); },
	function() { $(this).find('div').slideUp();	}
);
});
</script>

			<div id='the_dip_blog_by_seth_godin' class='module-typelist module'>
<h2 class='module-header'>THE DIP BLOG by Seth Godin</h2>
	<div class='module-content'>
		<ul class='module-list'>
							<li class='module-list-item'><a href='' ></a><br /><a href='http://www.squidoo.com/thedipbook' title='The Dip!'><img src='http://www.sethgodin.com/thedip/thedip.gif' width='150' height='218' border='0'/></a>
<!-- AddThis Feed Button END -->
<br>
<br>


</li>
			
		</ul>
	</div>
</div>

			
			<div id='all_marketers_are_liars_blog' class='module-typelist module'>
<h2 class='module-header'>All Marketers Are Liars Blog</h2>
	<div class='module-content'>
		<ul class='module-list'>
							<li class='module-list-item'><a href='' ></a><br /><a href='http://www.allmarketersareliars.com' title='All Marketers Are Liars'><img src='http://sethgodin.typepad.com/all_marketers_are_liars/the_book.gif' width='150' height='218' border='0'/></a>
<!-- AddThis Feed Button END -->
<br>
<br></li>
			
		</ul>
	</div>
</div>

			
			<!-- facebook like -->
			
			
			<!--$MTInclude module='category-cloud'$-->
			
			<!--$MTInclude module='elsewhere-grid'$-->
			
			<!--$MTInclude module='subscribe-to-feed'$-->
			
			<!-- powered by typepad -->
<div class='module-powered module'>
<div class='module-content'>
	<a href='http://www.typepad.com/' title='Blog'>Blog</a> powered by <a href='http://www.typepad.com/' title='TypePad'>TypePad</a><br />
	Member since 08/2003
</div><!-- .module-content -->
</div><!-- .module-powered .module -->

			
			</div><!-- #alpha-inner -->
		</div><!-- #alpha -->
		
		
		<div id='beta'>
			<div id='beta-inner' class='pkg'>
			
			<!-- entries -->
			
			
			
			<!-- this displays your posts, without any post you've marked as 'featured' -->
			
			<!--MTDateHeader>
				<h2 class='date-header'>May 11, 2016</h2>
			</MTDateHeader-->
			
			<div class='entry' id='entry-6a00d83451b31569e201bb085b3e7a970d'>
			
			<h3 class='entry-header first'>
				<a href='http://sethgodin.typepad.com/seths_blog/2016/05/striking-a-chord.html'>Striking a chord</a>
			</h3><!-- .entry-header -->
			
			<div class='entry-content'>
			
			<div class='entry-body'>
				<p>Commonly misunderstood and misspelled as &quot;striking a cord.&quot;</p>
<p>A cord is a single strand that connects. You can strike a cord, but not much happens.</p>
<p>A chord, on the other hand, is the resonance of multiple cords, more than one vibrating together.</p>
<p>That&#39;s rare, and worth seeking out.</p>
<p>It probably won&#39;t happen if you don&#39;t do it on purpose.</p>
			</div><!-- .entry-body -->
			
			
			
			</div><!-- .entry-content -->
			
			<div class='entry-footer'>
				<p class='entry-footer-info'>
			
				Posted by <a href='http://profile.typepad.com/sethgodin'>Seth Godin</a> on May 11, 2016
				
							
							
				<span class='separator'>|</span>
				
				<span class='entry-footer-permalink'><a href='http://sethgodin.typepad.com/seths_blog/2016/05/striking-a-chord.html'>Permalink</a></span>
				
				<!-- comment count -->
				
			
				
			
				</p><!-- .entry-footer-info -->

				
				
				<!-- post footer links -->
				<script src='http://platform.linkedin.com/in.js' type='text/javascript'></script>
<div class='share-counts'>
<p>
<span class='entry-footer-links-twitter'><iframe allowtransparency='true' frameborder='0' scrolling='no' src='http://platform.twitter.com/widgets/tweet_button.html?url=http://sethgodin.typepad.com/seths_blog/2016/05/striking-a-chord.html&text=Striking a chord&count=1' style='width:110px; height:20px;'></iframe></span> <span class='entry-footer-separator'>|</span> <span class='entry-footer-links-plusone'><g:plusone size='medium' href='http://sethgodin.typepad.com/seths_blog/2016/05/striking-a-chord.html' count='true'></g:plusone></span> <span class='entry-footer-separator'>|</span> <script type='IN/Share' data-counter='right' data-url='http://sethgodin.typepad.com/seths_blog/2016/05/striking-a-chord.html'></script> <span class='entry-footer-separator'>|</span> <span class='entry-footer-links-facebook_like'><iframe src='http://www.facebook.com/plugins/like.php?href=http://sethgodin.typepad.com/seths_blog/2016/05/striking-a-chord.html&amp;send=false&amp;layout=button_count&amp;show_faces=false&amp;action=like&amp;colorscheme=light&amp;font=arial&amp;height=21' scrolling='no' frameborder='0' style='border:none; overflow:visible; height:21px;' allowTransparency='true'></iframe></span>
</p></div>
				
			</div><!-- .entry-footer -->
			
			</div><!-- .entry -->
			
			
			
			<!-- this displays your posts, without any post you've marked as 'featured' -->
			
			<!--MTDateHeader>
				<h2 class='date-header'>May 10, 2016</h2>
			</MTDateHeader-->
			
			<div class='entry' id='entry-6a00d83451b31569e201bb08a5a2b4970d'>
			
			<h3 class='entry-header '>
				<a href='http://sethgodin.typepad.com/seths_blog/2016/05/the-problem-you-cant-talk-about.html'>The problem you can't talk about</a>
			</h3><!-- .entry-header -->
			
			<div class='entry-content'>
			
			<div class='entry-body'>
				<p>... is now two problems.</p>
			</div><!-- .entry-body -->
			
			
			
			</div><!-- .entry-content -->
			
			<div class='entry-footer'>
				<p class='entry-footer-info'>
			
				Posted by <a href='http://profile.typepad.com/sethgodin'>Seth Godin</a> on May 10, 2016
				
							
							
				<span class='separator'>|</span>
				
				<span class='entry-footer-permalink'><a href='http://sethgodin.typepad.com/seths_blog/2016/05/the-problem-you-cant-talk-about.html'>Permalink</a></span>
				
				<!-- comment count -->
				
			
				
			
				</p><!-- .entry-footer-info -->

				
				
				<!-- post footer links -->
				<script src='http://platform.linkedin.com/in.js' type='text/javascript'></script>
<div class='share-counts'>
<p>
<span class='entry-footer-links-twitter'><iframe allowtransparency='true' frameborder='0' scrolling='no' src='http://platform.twitter.com/widgets/tweet_button.html?url=http://sethgodin.typepad.com/seths_blog/2016/05/the-problem-you-cant-talk-about.html&text=The problem you can't talk about&count=1' style='width:110px; height:20px;'></iframe></span> <span class='entry-footer-separator'>|</span> <span class='entry-footer-links-plusone'><g:plusone size='medium' href='http://sethgodin.typepad.com/seths_blog/2016/05/the-problem-you-cant-talk-about.html' count='true'></g:plusone></span> <span class='entry-footer-separator'>|</span> <script type='IN/Share' data-counter='right' data-url='http://sethgodin.typepad.com/seths_blog/2016/05/the-problem-you-cant-talk-about.html'></script> <span class='entry-footer-separator'>|</span> <span class='entry-footer-links-facebook_like'><iframe src='http://www.facebook.com/plugins/like.php?href=http://sethgodin.typepad.com/seths_blog/2016/05/the-problem-you-cant-talk-about.html&amp;send=false&amp;layout=button_count&amp;show_faces=false&amp;action=like&amp;colorscheme=light&amp;font=arial&amp;height=21' scrolling='no' frameborder='0' style='border:none; overflow:visible; height:21px;' allowTransparency='true'></iframe></span>
</p></div>
				
			</div><!-- .entry-footer -->
			
			</div><!-- .entry -->
			
			
			
			<!-- this displays your posts, without any post you've marked as 'featured' -->
			
			<!--MTDateHeader>
				<h2 class='date-header'>May 09, 2016</h2>
			</MTDateHeader-->
			
			<div class='entry' id='entry-6a00d83451b31569e201b8d1df9511970c'>
			
			<h3 class='entry-header '>
				<a href='http://sethgodin.typepad.com/seths_blog/2016/05/avoid-being-treated-like-a-child.html'>On being treated like an adult</a>
			</h3><!-- .entry-header -->
			
			<div class='entry-content'>
			
			<div class='entry-body'>
				<p>It&#39;s great to dream like a kid, but no fun to be treated like one. It bristles because we feel that, even if the person involved has best intentions, we&#39;ve outgrown being treated like a child. Some behaviors to consider if you want to avoid this situation...</p>
<p>Make long-term plans instead of whining</p>
<p>Ask hard questions but accept truthful answers</p>
<p>Don&#39;t insist that there&#39;s a monster under the bed even after you&#39;ve seen there isn&#39;t</p>
<p>Manage your debt wisely</p>
<p>Go to school, early and often</p>
<p>Don&#39;t call people names</p>
<p>Get your own drink of water</p>
<p>Don&#39;t hit your&#0160;siblings</p>
<p>Stop bullying</p>
<p>No tantrums</p>
<p>(On the other hand, all the good stuff about being a kid helps you be happier and endear yourself to others: being filled with optimism and hope, smiling, trusting, finding creative solutions to old problems, hugging for no good reason, giggling and sharing your ice cream cone with a friend.)</p>
			</div><!-- .entry-body -->
			
			
			
			</div><!-- .entry-content -->
			
			<div class='entry-footer'>
				<p class='entry-footer-info'>
			
				Posted by <a href='http://profile.typepad.com/sethgodin'>Seth Godin</a> on May 09, 2016
				
							
							
				<span class='separator'>|</span>
				
				<span class='entry-footer-permalink'><a href='http://sethgodin.typepad.com/seths_blog/2016/05/avoid-being-treated-like-a-child.html'>Permalink</a></span>
				
				<!-- comment count -->
				
			
				
			
				</p><!-- .entry-footer-info -->

				
				
				<!-- post footer links -->
				<script src='http://platform.linkedin.com/in.js' type='text/javascript'></script>
<div class='share-counts'>
<p>
<span class='entry-footer-links-twitter'><iframe allowtransparency='true' frameborder='0' scrolling='no' src='http://platform.twitter.com/widgets/tweet_button.html?url=http://sethgodin.typepad.com/seths_blog/2016/05/avoid-being-treated-like-a-child.html&text=On being treated like an adult&count=1' style='width:110px; height:20px;'></iframe></span> <span class='entry-footer-separator'>|</span> <span class='entry-footer-links-plusone'><g:plusone size='medium' href='http://sethgodin.typepad.com/seths_blog/2016/05/avoid-being-treated-like-a-child.html' count='true'></g:plusone></span> <span class='entry-footer-separator'>|</span> <script type='IN/Share' data-counter='right' data-url='http://sethgodin.typepad.com/seths_blog/2016/05/avoid-being-treated-like-a-child.html'></script> <span class='entry-footer-separator'>|</span> <span class='entry-footer-links-facebook_like'><iframe src='http://www.facebook.com/plugins/like.php?href=http://sethgodin.typepad.com/seths_blog/2016/05/avoid-being-treated-like-a-child.html&amp;send=false&amp;layout=button_count&amp;show_faces=false&amp;action=like&amp;colorscheme=light&amp;font=arial&amp;height=21' scrolling='no' frameborder='0' style='border:none; overflow:visible; height:21px;' allowTransparency='true'></iframe></span>
</p></div>
				
			</div><!-- .entry-footer -->
			
			</div><!-- .entry -->
			
			
			
			<!-- this displays your posts, without any post you've marked as 'featured' -->
			
			<!--MTDateHeader>
				<h2 class='date-header'>May 08, 2016</h2>
			</MTDateHeader-->
			
			<div class='entry' id='entry-6a00d83451b31569e201b7c80084c9970b'>
			
			<h3 class='entry-header '>
				<a href='http://sethgodin.typepad.com/seths_blog/2016/05/rigor.html'>Rigor</a>
			</h3><!-- .entry-header -->
			
			<div class='entry-content'>
			
			<div class='entry-body'>
				<p>Doing things with rigor takes effort, but not everything you put effort into is done with rigor.</p>
<p>Rigor is a focus on process. Paying attention to not just how you do things, but why. Rigor requires us to never use an emergency as an excuse. It is a process for the long haul, the work of a professional.</p>
<p>An amateur bread baker leaves the kitchen coated in flour, and sometimes, perhaps, ends up with a great loaf of bread.</p>
<p>A professional baker might not seem to be as flustered, as hassled or even as busy. But the bread, the result of this mindful process, is worth buying, every day.</p>
<p>We know that you&#39;re working hard.&#0160;</p>
<p>The next step is to do it with rigor.</p>
			</div><!-- .entry-body -->
			
			
			
			</div><!-- .entry-content -->
			
			<div class='entry-footer'>
				<p class='entry-footer-info'>
			
				Posted by <a href='http://profile.typepad.com/sethgodin'>Seth Godin</a> on May 08, 2016
				
							
							
				<span class='separator'>|</span>
				
				<span class='entry-footer-permalink'><a href='http://sethgodin.typepad.com/seths_blog/2016/05/rigor.html'>Permalink</a></span>
				
				<!-- comment count -->
				
			
				
			
				</p><!-- .entry-footer-info -->

				
				
				<!-- post footer links -->
				<script src='http://platform.linkedin.com/in.js' type='text/javascript'></script>
<div class='share-counts'>
<p>
<span class='entry-footer-links-twitter'><iframe allowtransparency='true' frameborder='0' scrolling='no' src='http://platform.twitter.com/widgets/tweet_button.html?url=http://sethgodin.typepad.com/seths_blog/2016/05/rigor.html&text=Rigor&count=1' style='width:110px; height:20px;'></iframe></span> <span class='entry-footer-separator'>|</span> <span class='entry-footer-links-plusone'><g:plusone size='medium' href='http://sethgodin.typepad.com/seths_blog/2016/05/rigor.html' count='true'></g:plusone></span> <span class='entry-footer-separator'>|</span> <script type='IN/Share' data-counter='right' data-url='http://sethgodin.typepad.com/seths_blog/2016/05/rigor.html'></script> <span class='entry-footer-separator'>|</span> <span class='entry-footer-links-facebook_like'><iframe src='http://www.facebook.com/plugins/like.php?href=http://sethgodin.typepad.com/seths_blog/2016/05/rigor.html&amp;send=false&amp;layout=button_count&amp;show_faces=false&amp;action=like&amp;colorscheme=light&amp;font=arial&amp;height=21' scrolling='no' frameborder='0' style='border:none; overflow:visible; height:21px;' allowTransparency='true'></iframe></span>
</p></div>
				
			</div><!-- .entry-footer -->
			
			</div><!-- .entry -->
			
			
			
			<!-- this displays your posts, without any post you've marked as 'featured' -->
			
			<!--MTDateHeader>
				<h2 class='date-header'>May 07, 2016</h2>
			</MTDateHeader-->
			
			<div class='entry' id='entry-6a00d83451b31569e201b7c85459d5970b'>
			
			<h3 class='entry-header '>
				<a href='http://sethgodin.typepad.com/seths_blog/2016/05/calling-your-finding.html'>Calling your finding</a>
			</h3><!-- .entry-header -->
			
			<div class='entry-content'>
			
			<div class='entry-body'>
				<p>Many people are trying to find their calling.</p>
<p>But that doesn&#39;t explain Marianne Money, bank manager, or Jim Kardwell, who owns a card company. Or Thomas Duck who started Ugly Duckling rent-a-car and Tito Beveridge who makes vodka. It doesn&#39;t explain why people named Dennis are more likely to become dentists...</p>
<p>I&#39;m not sure that <em>anyone</em> has a calling. I think, instead, our culture creates situations where passionate people find a place where they can make an impact. When what you do is something that you make important, it doesn&#39;t matter so much what you do.</p>
<p>It&#39;s not that important <em>where</em>. It matters a lot <em>how</em>. With passion and care.</p>
			</div><!-- .entry-body -->
			
			
			
			</div><!-- .entry-content -->
			
			<div class='entry-footer'>
				<p class='entry-footer-info'>
			
				Posted by <a href='http://profile.typepad.com/sethgodin'>Seth Godin</a> on May 07, 2016
				
							
							
				<span class='separator'>|</span>
				
				<span class='entry-footer-permalink'><a href='http://sethgodin.typepad.com/seths_blog/2016/05/calling-your-finding.html'>Permalink</a></span>
				
				<!-- comment count -->
				
			
				
			
				</p><!-- .entry-footer-info -->

				
				
				<!-- post footer links -->
				<script src='http://platform.linkedin.com/in.js' type='text/javascript'></script>
<div class='share-counts'>
<p>
<span class='entry-footer-links-twitter'><iframe allowtransparency='true' frameborder='0' scrolling='no' src='http://platform.twitter.com/widgets/tweet_button.html?url=http://sethgodin.typepad.com/seths_blog/2016/05/calling-your-finding.html&text=Calling your finding&count=1' style='width:110px; height:20px;'></iframe></span> <span class='entry-footer-separator'>|</span> <span class='entry-footer-links-plusone'><g:plusone size='medium' href='http://sethgodin.typepad.com/seths_blog/2016/05/calling-your-finding.html' count='true'></g:plusone></span> <span class='entry-footer-separator'>|</span> <script type='IN/Share' data-counter='right' data-url='http://sethgodin.typepad.com/seths_blog/2016/05/calling-your-finding.html'></script> <span class='entry-footer-separator'>|</span> <span class='entry-footer-links-facebook_like'><iframe src='http://www.facebook.com/plugins/like.php?href=http://sethgodin.typepad.com/seths_blog/2016/05/calling-your-finding.html&amp;send=false&amp;layout=button_count&amp;show_faces=false&amp;action=like&amp;colorscheme=light&amp;font=arial&amp;height=21' scrolling='no' frameborder='0' style='border:none; overflow:visible; height:21px;' allowTransparency='true'></iframe></span>
</p></div>
				
			</div><!-- .entry-footer -->
			
			</div><!-- .entry -->
			
			
			
			<!-- this displays your posts, without any post you've marked as 'featured' -->
			
			<!--MTDateHeader>
				<h2 class='date-header'>May 06, 2016</h2>
			</MTDateHeader-->
			
			<div class='entry' id='entry-6a00d83451b31569e201bb08f4e4eb970d'>
			
			<h3 class='entry-header '>
				<a href='http://sethgodin.typepad.com/seths_blog/2016/05/unlimited-bowling.html'>Unlimited bowling</a>
			</h3><!-- .entry-header -->
			
			<div class='entry-content'>
			
			<div class='entry-body'>
				<p>When we were kids, my&#0160;mom, fully exasperated, would&#0160;survive a day when school was closed by dropping a bunch of us off at Sheridan Lanes for a few hours of bowling.</p>
<p>You only had a certain amount of money to spend, and each game (and the snacks) cost, so we&#0160;knew that one&#0160;could only play a few games. Which meant that every single roll mattered. Don&#39;t waste one.</p>
<p>Unlimited bowling is a whole different concept. As many games as you want. Roll to your heart&#39;s content.</p>
<p>When you&#39;re doing unlimited bowling, you can practice various shots. You can work on the risky splits. You can bowl without remorse.</p>
<p>As you&#39;ve guessed, the fat pipes of the internet bring the idea of unlimited bowling to much of what we do. Interesting is enough. Generous is enough. Learning is enough.</p>
<p>It&#39;s a special kind of freedom, we shouldn&#39;t waste it.</p>
<p>More on this in my new&#0160;<a href='https://www.creativelive.com/30-days-of-genius/seth-godin' target='_blank'>interview</a> with Chase Jarvis. (<a href='https://www.youtube.com/watch?v=6xMxAZhgVvU' target='_blank'>YouTube</a>)</p>
			</div><!-- .entry-body -->
			
			
			
			</div><!-- .entry-content -->
			
			<div class='entry-footer'>
				<p class='entry-footer-info'>
			
				Posted by <a href='http://profile.typepad.com/sethgodin'>Seth Godin</a> on May 06, 2016
				
							
							
				<span class='separator'>|</span>
				
				<span class='entry-footer-permalink'><a href='http://sethgodin.typepad.com/seths_blog/2016/05/unlimited-bowling.html'>Permalink</a></span>
				
				<!-- comment count -->
				
			
				
			
				</p><!-- .entry-footer-info -->

				
				
				<!-- post footer links -->
				<script src='http://platform.linkedin.com/in.js' type='text/javascript'></script>
<div class='share-counts'>
<p>
<span class='entry-footer-links-twitter'><iframe allowtransparency='true' frameborder='0' scrolling='no' src='http://platform.twitter.com/widgets/tweet_button.html?url=http://sethgodin.typepad.com/seths_blog/2016/05/unlimited-bowling.html&text=Unlimited bowling&count=1' style='width:110px; height:20px;'></iframe></span> <span class='entry-footer-separator'>|</span> <span class='entry-footer-links-plusone'><g:plusone size='medium' href='http://sethgodin.typepad.com/seths_blog/2016/05/unlimited-bowling.html' count='true'></g:plusone></span> <span class='entry-footer-separator'>|</span> <script type='IN/Share' data-counter='right' data-url='http://sethgodin.typepad.com/seths_blog/2016/05/unlimited-bowling.html'></script> <span class='entry-footer-separator'>|</span> <span class='entry-footer-links-facebook_like'><iframe src='http://www.facebook.com/plugins/like.php?href=http://sethgodin.typepad.com/seths_blog/2016/05/unlimited-bowling.html&amp;send=false&amp;layout=button_count&amp;show_faces=false&amp;action=like&amp;colorscheme=light&amp;font=arial&amp;height=21' scrolling='no' frameborder='0' style='border:none; overflow:visible; height:21px;' allowTransparency='true'></iframe></span>
</p></div>
				
			</div><!-- .entry-footer -->
			
			</div><!-- .entry -->
			
			
			
			<!-- this displays your posts, without any post you've marked as 'featured' -->
			
			<!--MTDateHeader>
				<h2 class='date-header'>May 05, 2016</h2>
			</MTDateHeader-->
			
			<div class='entry' id='entry-6a00d83451b31569e201b8d1da72ed970c'>
			
			<h3 class='entry-header '>
				<a href='http://sethgodin.typepad.com/seths_blog/2016/05/the-most-common-b2b-objection.html'>The most common b2b objection (and the one we have about most innovations)</a>
			</h3><!-- .entry-header -->
			
			<div class='entry-content'>
			
			<div class='entry-body'>
				<p>You&#39;ll never hear it spoken aloud, but it happens all the time, particularly when you&#39;re selling something new, something powerful, something that causes a positive change:</p>
<p>&quot;You&#39;re right, but we&#39;re not ready.&quot;</p>
<p>This is what people felt about the internet, about word processors, about yoga pants...</p>
<p>When you think this is going on, the answer isn&#39;t to be more &#39;right&#39;. The answer is to figure out how to help people be more &#39;ready&#39;.</p>
<p>PS I&#39;m doing an AMAAA (ask me anything about the altMBA) today at 3&#0160;pm NY time.</p>
<p>Find out more by <a href='https://altmba.com/keepmeposted' target='_blank'>subscribing</a> to&#0160;the altMBA&#0160;newsletter today and we&#39;ll send you&#0160;all the details about the info session.</p>
			</div><!-- .entry-body -->
			
			
			
			</div><!-- .entry-content -->
			
			<div class='entry-footer'>
				<p class='entry-footer-info'>
			
				Posted by <a href='http://profile.typepad.com/sethgodin'>Seth Godin</a> on May 05, 2016
				
							
							
				<span class='separator'>|</span>
				
				<span class='entry-footer-permalink'><a href='http://sethgodin.typepad.com/seths_blog/2016/05/the-most-common-b2b-objection.html'>Permalink</a></span>
				
				<!-- comment count -->
				
			
				
			
				</p><!-- .entry-footer-info -->

				
				
				<!-- post footer links -->
				<script src='http://platform.linkedin.com/in.js' type='text/javascript'></script>
<div class='share-counts'>
<p>
<span class='entry-footer-links-twitter'><iframe allowtransparency='true' frameborder='0' scrolling='no' src='http://platform.twitter.com/widgets/tweet_button.html?url=http://sethgodin.typepad.com/seths_blog/2016/05/the-most-common-b2b-objection.html&text=The most common b2b objection (and the one we have about most innovations)&count=1' style='width:110px; height:20px;'></iframe></span> <span class='entry-footer-separator'>|</span> <span class='entry-footer-links-plusone'><g:plusone size='medium' href='http://sethgodin.typepad.com/seths_blog/2016/05/the-most-common-b2b-objection.html' count='true'></g:plusone></span> <span class='entry-footer-separator'>|</span> <script type='IN/Share' data-counter='right' data-url='http://sethgodin.typepad.com/seths_blog/2016/05/the-most-common-b2b-objection.html'></script> <span class='entry-footer-separator'>|</span> <span class='entry-footer-links-facebook_like'><iframe src='http://www.facebook.com/plugins/like.php?href=http://sethgodin.typepad.com/seths_blog/2016/05/the-most-common-b2b-objection.html&amp;send=false&amp;layout=button_count&amp;show_faces=false&amp;action=like&amp;colorscheme=light&amp;font=arial&amp;height=21' scrolling='no' frameborder='0' style='border:none; overflow:visible; height:21px;' allowTransparency='true'></iframe></span>
</p></div>
				
			</div><!-- .entry-footer -->
			
			</div><!-- .entry -->
			
			
			
			<!-- this displays your posts, without any post you've marked as 'featured' -->
			
			<!--MTDateHeader>
				<h2 class='date-header'>May 04, 2016</h2>
			</MTDateHeader-->
			
			<div class='entry' id='entry-6a00d83451b31569e201b7c742c3ef970b'>
			
			<h3 class='entry-header '>
				<a href='http://sethgodin.typepad.com/seths_blog/2016/05/what-do-i-owe-you.html'>'What do I owe you?'</a>
			</h3><!-- .entry-header -->
			
			<div class='entry-content'>
			
			<div class='entry-body'>
				<p>One of the little-remembered innovations of the industrial economy was the price tag.</p>
<p>If it was for sale, you knew how much it cost.</p>
<p>And if you got a job, you knew what you got paid--by the piece, at first, and then by the hour and perhaps by the week.</p>
<p>Both price tags and pre-agreed wages are pretty new ideas, ideas that fundamentally changed our culture.</p>
<p>By putting a price on buying and selling of goods and effort, industrialists permitted commerce to flow. One of the side effects, as Lewis Hyde has pointed out, is that knowing the price depersonalizes the transaction. It&#39;s even steven, we&#39;re done, goodbye.</p>
<p>Compare this to the craftsperson who won&#39;t sell to someone she doesn&#39;t respect, or the cook who charges people based on what he thinks someone can afford, or based on what he&#39;ll need to keep this project going a little longer... These ad hoc transactions are personal, they bring us closer together. Everything doesn&#39;t have to have a price if we don&#39;t let it.</p>
<p>Which leads to the eagerly avoided questions like, &quot;What do you owe the editors at Wikipedia?&quot; or &quot;Is it okay to blog if you don&#39;t get paid for it?&quot; and &quot;Is there a difference between staying at a friend of a friend&#39;s house and staying at an Airbnb?&quot; When people use Kickstarter as a sort of store, they denature the entire point of the exercise.</p>
<p>Seeking out personal transactions might be merely a clever way to save money. But in a post-industrial economy, it&#39;s also a way to pay it forward and to build community.</p>
<p>Sometimes, we don&#39;t pay because we have to, we pay because we can.</p>
<p><strong>[PS... a new course, on listening]</strong></p>
<p>The third Acumen course is now live... the astonishing <a href='http://plusacumen.org/acumen-master-krista-tippett/' target='_blank'>Krista Tippett</a> is doing her first online course, and you can find it here at a discount. (Trouble with the link? Please try: <a href='http://plusacumen.org/acumen-master-krista-tippett/' target='_blank'>http://plusacumen.org/acumen-master-krista-tippett/</a>&#0160;)</p>
<p>This joins the course we did with <a href='http://plusacumen.org/acumen-master-class-elizabeth-gilberts-creativity-workshop/' target='_blank'>Elizabeth Gilbert</a> (see below for reviews).</p>
<p>Which followed&#0160;the first, the <a href='http://plusacumen.org/acumen-presents-seth-godins-leadership-workshop/' target='_blank'>leadership course</a> I launched the series with.</p>
<p>It&#39;s amazing what you can learn in a few hours if you&#39;re willing to do the work.</p>
<p>&#0160;* * *</p>
<p>Elizabeth is awesome on camera. I feel like it&#39;s just the two of us. Normally, I hate online courses. This is different! Loving this! - Denise</p>
<p>Who doesn&#39;t love Liz Gilbert? The content was refreshing and inspirational. The assignments were thought-provoking. For the price I paid, I thought this was a great workshop. - Bernadette Xiong</p>
<p>This is amazing. I have needed this kind of talking to for a very long time. Thank you, Elizabeth. - James Hoag</p>
<p>I love it! Her voice is soothing and what she is saying is so appealing. I can&#39;t wait to go on! - Susan Archibald</p>
<p>I enjoyed it very much. Many good nuggets of wisdom to help me on my path. - Linda Joyner</p>
<p>Elizabeth has that rare ability to invite you into an intimate conversation on a very weighty subject, with a touch as light as a sparrow&#39;s ripple of air on a spring day. The introduction has already laid out some actions to take that I can tell will wake up my sense of being alive and in the world. - Jim Caroompas</p>
<p>Being at the age where you start questioning everything around you, I feel so far that this workshop is directed to me. I feel as thought Liz has invited me over to discuss a few things to help me get back on track. – Maria Pezzano<br /><br />Liz&#39;s response to the fatigued teacher really resonated with me. The fact that the reason and season for our existence and the various roles we play change with time. I love the takeaways - going from grandiose to granular, learning with humility and serving with joy. These are lessons for life. – Smita Kumar<br /><br />This course was just what I needed, delivered by a wise, empathetic, funny, fun Elizabeth Gilbert. It didn&#39;t chew up vast amounts of time or make me feel like I had &quot;work&quot; to do. I enjoyed it so much I&#39;ll probably go back and do the entire thing over again. Don&#39;t feel like you need to do all the workbooks right away, either. I percolated them for a while and it still worked out fine. More Elizabeth Gilbert, please! – Vanessa Kelly</p>
			</div><!-- .entry-body -->
			
			
			
			</div><!-- .entry-content -->
			
			<div class='entry-footer'>
				<p class='entry-footer-info'>
			
				Posted by <a href='http://profile.typepad.com/sethgodin'>Seth Godin</a> on May 04, 2016
				
							
							
				<span class='separator'>|</span>
				
				<span class='entry-footer-permalink'><a href='http://sethgodin.typepad.com/seths_blog/2016/05/what-do-i-owe-you.html'>Permalink</a></span>
				
				<!-- comment count -->
				
			
				
			
				</p><!-- .entry-footer-info -->

				
				
				<!-- post footer links -->
				<script src='http://platform.linkedin.com/in.js' type='text/javascript'></script>
<div class='share-counts'>
<p>
<span class='entry-footer-links-twitter'><iframe allowtransparency='true' frameborder='0' scrolling='no' src='http://platform.twitter.com/widgets/tweet_button.html?url=http://sethgodin.typepad.com/seths_blog/2016/05/what-do-i-owe-you.html&text='What do I owe you?'&count=1' style='width:110px; height:20px;'></iframe></span> <span class='entry-footer-separator'>|</span> <span class='entry-footer-links-plusone'><g:plusone size='medium' href='http://sethgodin.typepad.com/seths_blog/2016/05/what-do-i-owe-you.html' count='true'></g:plusone></span> <span class='entry-footer-separator'>|</span> <script type='IN/Share' data-counter='right' data-url='http://sethgodin.typepad.com/seths_blog/2016/05/what-do-i-owe-you.html'></script> <span class='entry-footer-separator'>|</span> <span class='entry-footer-links-facebook_like'><iframe src='http://www.facebook.com/plugins/like.php?href=http://sethgodin.typepad.com/seths_blog/2016/05/what-do-i-owe-you.html&amp;send=false&amp;layout=button_count&amp;show_faces=false&amp;action=like&amp;colorscheme=light&amp;font=arial&amp;height=21' scrolling='no' frameborder='0' style='border:none; overflow:visible; height:21px;' allowTransparency='true'></iframe></span>
</p></div>
				
			</div><!-- .entry-footer -->
			
			</div><!-- .entry -->
			
			
			
			<!-- this displays your posts, without any post you've marked as 'featured' -->
			
			<!--MTDateHeader>
				<h2 class='date-header'>May 03, 2016</h2>
			</MTDateHeader-->
			
			<div class='entry' id='entry-6a00d83451b31569e201bb089770b5970d'>
			
			<h3 class='entry-header '>
				<a href='http://sethgodin.typepad.com/seths_blog/2016/05/learning-from-the-rejection.html'>Learning from the rejection</a>
			</h3><!-- .entry-header -->
			
			<div class='entry-content'>
			
			<div class='entry-body'>
				<p>When someone doesn&#39;t say yes, they&#39;ll often give you a reason.</p>
<p>A common&#0160;trap: Believe the reason.</p>
<p>If you start rebuilding your product, your pitch and your PR based on the stated reason, you&#39;re driving by looking in the rear view mirror.</p>
<p>The people who turn you down have a reason, but they&#39;re almost certainly not telling you why.</p>
<p>Fake reasons: I don&#39;t like the color, it&#39;s too expensive, you don&#39;t have enough references, there was a typo in your resume.</p>
<p>Real reasons: My boss won&#39;t let me, I don&#39;t trust you, I&#39;m afraid of change.</p>
<p>By all means, make your stuff better. More important, focus on the unstated reasons that drive most rejections. And most important: Shun the non-believers and sell to people who want to go on a journey with you.</p>
			</div><!-- .entry-body -->
			
			
			
			</div><!-- .entry-content -->
			
			<div class='entry-footer'>
				<p class='entry-footer-info'>
			
				Posted by <a href='http://profile.typepad.com/sethgodin'>Seth Godin</a> on May 03, 2016
				
							
							
				<span class='separator'>|</span>
				
				<span class='entry-footer-permalink'><a href='http://sethgodin.typepad.com/seths_blog/2016/05/learning-from-the-rejection.html'>Permalink</a></span>
				
				<!-- comment count -->
				
			
				
			
				</p><!-- .entry-footer-info -->

				
				
				<!-- post footer links -->
				<script src='http://platform.linkedin.com/in.js' type='text/javascript'></script>
<div class='share-counts'>
<p>
<span class='entry-footer-links-twitter'><iframe allowtransparency='true' frameborder='0' scrolling='no' src='http://platform.twitter.com/widgets/tweet_button.html?url=http://sethgodin.typepad.com/seths_blog/2016/05/learning-from-the-rejection.html&text=Learning from the rejection&count=1' style='width:110px; height:20px;'></iframe></span> <span class='entry-footer-separator'>|</span> <span class='entry-footer-links-plusone'><g:plusone size='medium' href='http://sethgodin.typepad.com/seths_blog/2016/05/learning-from-the-rejection.html' count='true'></g:plusone></span> <span class='entry-footer-separator'>|</span> <script type='IN/Share' data-counter='right' data-url='http://sethgodin.typepad.com/seths_blog/2016/05/learning-from-the-rejection.html'></script> <span class='entry-footer-separator'>|</span> <span class='entry-footer-links-facebook_like'><iframe src='http://www.facebook.com/plugins/like.php?href=http://sethgodin.typepad.com/seths_blog/2016/05/learning-from-the-rejection.html&amp;send=false&amp;layout=button_count&amp;show_faces=false&amp;action=like&amp;colorscheme=light&amp;font=arial&amp;height=21' scrolling='no' frameborder='0' style='border:none; overflow:visible; height:21px;' allowTransparency='true'></iframe></span>
</p></div>
				
			</div><!-- .entry-footer -->
			
			</div><!-- .entry -->
			
			
			
			<!-- this displays your posts, without any post you've marked as 'featured' -->
			
			<!--MTDateHeader>
				<h2 class='date-header'>May 02, 2016</h2>
			</MTDateHeader-->
			
			<div class='entry' id='entry-6a00d83451b31569e201b8d1d22bc4970c'>
			
			<h3 class='entry-header '>
				<a href='http://sethgodin.typepad.com/seths_blog/2016/05/duck.html'>Duck!</a>
			</h3><!-- .entry-header -->
			
			<div class='entry-content'>
			
			<div class='entry-body'>
				<p>Perhaps you can&#39;t see it, but we can. That 2 x 4, the board set&#0160;right across that doorway, about 5 feet off the ground.</p>
<p>You&#39;re running it at it full speed, and in a moment, you&#39;re going to slam into it, which is going to hurt, a lot.</p>
<p>This happens to most of us, metaphorically anyway, at one time or another. But when it happens repeatedly, you probably have a hygiene problem.</p>
<p>Emotional hygiene, personal hygiene, moral hygiene, organizational hygiene--useful&#0160;terms&#0160;for the act of deliberately making hard decisions, early and often, to prevent a 2 x 4 to the face later.</p>
<p>Worth a pause to highlight that: hygiene never pays off in the short run. It is always the work of a mature person (or &#0160;an organization) who cares enough about the later to do something important in the now.</p>
<p>When the doctor scrubs with soap before a procedure, it&#39;s not because it&#39;s fun. It&#39;s because she&#39;s investing a few minutes now to prevent sepsis later.</p>
<p>Way better than getting hit in the face with a 2 x 4.</p>
			</div><!-- .entry-body -->
			
			
			
			</div><!-- .entry-content -->
			
			<div class='entry-footer'>
				<p class='entry-footer-info'>
			
				Posted by <a href='http://profile.typepad.com/sethgodin'>Seth Godin</a> on May 02, 2016
				
							
							
				<span class='separator'>|</span>
				
				<span class='entry-footer-permalink'><a href='http://sethgodin.typepad.com/seths_blog/2016/05/duck.html'>Permalink</a></span>
				
				<!-- comment count -->
				
			
				
			
				</p><!-- .entry-footer-info -->

				
				
				<!-- post footer links -->
				<script src='http://platform.linkedin.com/in.js' type='text/javascript'></script>
<div class='share-counts'>
<p>
<span class='entry-footer-links-twitter'><iframe allowtransparency='true' frameborder='0' scrolling='no' src='http://platform.twitter.com/widgets/tweet_button.html?url=http://sethgodin.typepad.com/seths_blog/2016/05/duck.html&text=Duck!&count=1' style='width:110px; height:20px;'></iframe></span> <span class='entry-footer-separator'>|</span> <span class='entry-footer-links-plusone'><g:plusone size='medium' href='http://sethgodin.typepad.com/seths_blog/2016/05/duck.html' count='true'></g:plusone></span> <span class='entry-footer-separator'>|</span> <script type='IN/Share' data-counter='right' data-url='http://sethgodin.typepad.com/seths_blog/2016/05/duck.html'></script> <span class='entry-footer-separator'>|</span> <span class='entry-footer-links-facebook_like'><iframe src='http://www.facebook.com/plugins/like.php?href=http://sethgodin.typepad.com/seths_blog/2016/05/duck.html&amp;send=false&amp;layout=button_count&amp;show_faces=false&amp;action=like&amp;colorscheme=light&amp;font=arial&amp;height=21' scrolling='no' frameborder='0' style='border:none; overflow:visible; height:21px;' allowTransparency='true'></iframe></span>
</p></div>
				
			</div><!-- .entry-footer -->
			
			</div><!-- .entry -->
			
			
			
			<!-- this displays your posts, without any post you've marked as 'featured' -->
			
			<!--MTDateHeader>
				<h2 class='date-header'>May 01, 2016</h2>
			</MTDateHeader-->
			
			<div class='entry' id='entry-6a00d83451b31569e201b8d1d81df5970c'>
			
			<h3 class='entry-header '>
				<a href='http://sethgodin.typepad.com/seths_blog/2016/05/how-to-use-a-microphone.html'>How to use a microphone</a>
			</h3><!-- .entry-header -->
			
			<div class='entry-content'>
			
			<div class='entry-body'>
				<p>More than 10,000 people attended the Lincoln Douglas debates, and yet they debated without amplification.</p>
<p>It&#39;s only quite recently that we began to disassociate talking-to-many from&#0160;talking loudly. Having a large and varied audience used to mean yelling, it used to be physically taxing, it would put our entire body on alert.</p>
<p>Now, of course, all of us have a microphone.</p>
<p>The instinct remains, though. When we know that hundreds or thousands of people will read our words online, we tense up. When we get on stage, we follow that pattern and tense our vocal cords.</p>
<p>We shout.</p>
<p>The problem with shouting is that it pushes people away. WHEN YOU SHOUT IN EMAIL, IT SEEMS ANGRY. Shouting creates a wall between us and the person at the other end (even though it seems like many people, sooner or later, there&#39;s one person at the other end).&#0160;</p>
<p>Shouting destroys intimacy, and it hurts our impact, the impact that comes from authenticity.</p>
<p>We feel speech and words long before we hear the words, and we hear the words long before we understand them.</p>
<p>The solution is simple: whisper.</p>
<p>Practice whispering.</p>
<p>Whisper when you type, whisper when you address a meeting.</p>
<p>Lower your voice, slow your pace, and talk more quietly.</p>
<p>The microphone will amplify your words. And we&#39;ll hear them.&#0160;</p>
			</div><!-- .entry-body -->
			
			
			
			</div><!-- .entry-content -->
			
			<div class='entry-footer'>
				<p class='entry-footer-info'>
			
				Posted by <a href='http://profile.typepad.com/sethgodin'>Seth Godin</a> on May 01, 2016
				
							
							
				<span class='separator'>|</span>
				
				<span class='entry-footer-permalink'><a href='http://sethgodin.typepad.com/seths_blog/2016/05/how-to-use-a-microphone.html'>Permalink</a></span>
				
				<!-- comment count -->
				
			
				
			
				</p><!-- .entry-footer-info -->

				
				
				<!-- post footer links -->
				<script src='http://platform.linkedin.com/in.js' type='text/javascript'></script>
<div class='share-counts'>
<p>
<span class='entry-footer-links-twitter'><iframe allowtransparency='true' frameborder='0' scrolling='no' src='http://platform.twitter.com/widgets/tweet_button.html?url=http://sethgodin.typepad.com/seths_blog/2016/05/how-to-use-a-microphone.html&text=How to use a microphone&count=1' style='width:110px; height:20px;'></iframe></span> <span class='entry-footer-separator'>|</span> <span class='entry-footer-links-plusone'><g:plusone size='medium' href='http://sethgodin.typepad.com/seths_blog/2016/05/how-to-use-a-microphone.html' count='true'></g:plusone></span> <span class='entry-footer-separator'>|</span> <script type='IN/Share' data-counter='right' data-url='http://sethgodin.typepad.com/seths_blog/2016/05/how-to-use-a-microphone.html'></script> <span class='entry-footer-separator'>|</span> <span class='entry-footer-links-facebook_like'><iframe src='http://www.facebook.com/plugins/like.php?href=http://sethgodin.typepad.com/seths_blog/2016/05/how-to-use-a-microphone.html&amp;send=false&amp;layout=button_count&amp;show_faces=false&amp;action=like&amp;colorscheme=light&amp;font=arial&amp;height=21' scrolling='no' frameborder='0' style='border:none; overflow:visible; height:21px;' allowTransparency='true'></iframe></span>
</p></div>
				
			</div><!-- .entry-footer -->
			
			</div><!-- .entry -->
			
			
			
			<!-- this displays your posts, without any post you've marked as 'featured' -->
			
			<!--MTDateHeader>
				<h2 class='date-header'>April 30, 2016</h2>
			</MTDateHeader-->
			
			<div class='entry' id='entry-6a00d83451b31569e201bb08f1c2dd970d'>
			
			<h3 class='entry-header '>
				<a href='http://sethgodin.typepad.com/seths_blog/2016/04/errors-in-scale.html'>Errors in scale</a>
			</h3><!-- .entry-header -->
			
			<div class='entry-content'>
			
			<div class='entry-body'>
				<p>A restaurant that&#39;s too small for its following creates pent-up demand and can thrive as it lays plans to expand.</p>
<p>A restaurant that&#39;s too big merely fails.</p>
<p>There are occasional counterexamples of ventures that fail because they were&#0160;too small when they gained customer traction. But not many.</p>
<p>It pays to have big dreams but low overhead.</p>
			</div><!-- .entry-body -->
			
			
			
			</div><!-- .entry-content -->
			
			<div class='entry-footer'>
				<p class='entry-footer-info'>
			
				Posted by <a href='http://profile.typepad.com/sethgodin'>Seth Godin</a> on April 30, 2016
				
							
							
				<span class='separator'>|</span>
				
				<span class='entry-footer-permalink'><a href='http://sethgodin.typepad.com/seths_blog/2016/04/errors-in-scale.html'>Permalink</a></span>
				
				<!-- comment count -->
				
			
				
			
				</p><!-- .entry-footer-info -->

				
				
				<!-- post footer links -->
				<script src='http://platform.linkedin.com/in.js' type='text/javascript'></script>
<div class='share-counts'>
<p>
<span class='entry-footer-links-twitter'><iframe allowtransparency='true' frameborder='0' scrolling='no' src='http://platform.twitter.com/widgets/tweet_button.html?url=http://sethgodin.typepad.com/seths_blog/2016/04/errors-in-scale.html&text=Errors in scale&count=1' style='width:110px; height:20px;'></iframe></span> <span class='entry-footer-separator'>|</span> <span class='entry-footer-links-plusone'><g:plusone size='medium' href='http://sethgodin.typepad.com/seths_blog/2016/04/errors-in-scale.html' count='true'></g:plusone></span> <span class='entry-footer-separator'>|</span> <script type='IN/Share' data-counter='right' data-url='http://sethgodin.typepad.com/seths_blog/2016/04/errors-in-scale.html'></script> <span class='entry-footer-separator'>|</span> <span class='entry-footer-links-facebook_like'><iframe src='http://www.facebook.com/plugins/like.php?href=http://sethgodin.typepad.com/seths_blog/2016/04/errors-in-scale.html&amp;send=false&amp;layout=button_count&amp;show_faces=false&amp;action=like&amp;colorscheme=light&amp;font=arial&amp;height=21' scrolling='no' frameborder='0' style='border:none; overflow:visible; height:21px;' allowTransparency='true'></iframe></span>
</p></div>
				
			</div><!-- .entry-footer -->
			
			</div><!-- .entry -->
			
			
			
			<!-- this displays your posts, without any post you've marked as 'featured' -->
			
			<!--MTDateHeader>
				<h2 class='date-header'>April 29, 2016</h2>
			</MTDateHeader-->
			
			<div class='entry' id='entry-6a00d83451b31569e201b7c84d046f970b'>
			
			<h3 class='entry-header '>
				<a href='http://sethgodin.typepad.com/seths_blog/2016/04/your-money-and-your-future.html'>Your money and your future</a>
			</h3><!-- .entry-header -->
			
			<div class='entry-content'>
			
			<div class='entry-body'>
				<p><strong>Your money:</strong> Almost no one knows how to think about money and investing. Squadrons of people will try to confuse you and rip you off. Many will bore you. But Andrew Tobias has written a book that might just change your net worth.</p>
<p>His advice is simple: spending less is even more valuable than earning more. He is also a gifted writer, funny and dead on correct in his analysis. Highly recommended.</p>
<p>The brand new edition is <a href='http://smile.amazon.com/Only-Investment-Guide-Youll-Ever/dp/0544781937/ref=sr_1_1?ie=UTF8&amp;qid=1461890610&amp;sr=8-1&amp;keywords=andrew+tobias+only+investment' target='_blank'>right here</a>.</p>
<p>Back story: 32 years ago this month, I had lunch with Andy Tobias. I was pitching him on a partnership, and the meeting had been difficult to get. I was intimidated and soaking wet from running fifty blocks through Manhattan (no Uber!). As I sat in the New York Athletic Club, my cheap suit dripping wet (you can&#39;t take off your jacket at the New York Athletic Club), I tried to break the ice by telling the <a href='https://www.youtube.com/watch?v=xmnLRVWgnXU' target='_blank'>moose joke.</a></p>
<p>I told it pretty well, but Andy didn&#39;t crack a smile. Even then, he was a canny negotiator. We never ended up working together, but his book probably did me more good than the project would have. And the story was priceless.</p>
<p><strong>Your future:</strong> Kevin Kelly is the most erudite, original and prophetic futurist of our time. If you&#39;ve ever picked up a copy of <em>Wired</em>, he&#39;s had an impact on your life.</p>
<p>If you hope to be working, producing value or merely alive in ten years, his <a href='http://smile.amazon.com/Inevitable-Understanding-Technological-Forces-Future/dp/0525428089/ref=sr_1_1?ie=UTF8&amp;qid=1461890462&amp;sr=8-1&amp;keywords=kevin+kelly+inevitable' target='_blank'>new book</a> (out in June) is essential. It might take you an hour or two to read certain pages—if you&#39;re smart enough to take notes and brainstorm as you go.</p>
<p>The people who read his previous book about the future (<em><a href='http://kk.org/newrules/' target='_blank'>New Rules</a></em>) in 1998 are truly grateful for the decade-long head start it gave them.</p>
<p>I&#39;ve never had the nerve to tell Kevin a joke, but I did <a href='http://boingboing.net/2014/11/03/seth-godin-recommends-his-favo.html' target='_blank'>offer to do a magic trick</a>&#0160;for him.</p>
<p>It&#39;s rare that you can spend $33 on two books and have your life so profoundly altered.</p>
<p>PS new Creative Mornings <a href='https://creativemornings.com/podcast/episodes/seth-godin' target='_blank'>podcast</a> just up with my talk from a few years ago.</p>
<p><em>Backwards</em>: Great designers don&#39;t get great clients, it&#39;s the other way around.</p>
<p>Patience is for the impatient.</p>
<p>Leading up is&#0160;more powerful than the alternative.</p>
<p>...And a few more provocations. I only gave this talk once, I hope you enjoy it.</p>
			</div><!-- .entry-body -->
			
			
			
			</div><!-- .entry-content -->
			
			<div class='entry-footer'>
				<p class='entry-footer-info'>
			
				Posted by <a href='http://profile.typepad.com/sethgodin'>Seth Godin</a> on April 29, 2016
				
							
							
				<span class='separator'>|</span>
				
				<span class='entry-footer-permalink'><a href='http://sethgodin.typepad.com/seths_blog/2016/04/your-money-and-your-future.html'>Permalink</a></span>
				
				<!-- comment count -->
				
			
				
			
				</p><!-- .entry-footer-info -->

				
				
				<!-- post footer links -->
				<script src='http://platform.linkedin.com/in.js' type='text/javascript'></script>
<div class='share-counts'>
<p>
<span class='entry-footer-links-twitter'><iframe allowtransparency='true' frameborder='0' scrolling='no' src='http://platform.twitter.com/widgets/tweet_button.html?url=http://sethgodin.typepad.com/seths_blog/2016/04/your-money-and-your-future.html&text=Your money and your future&count=1' style='width:110px; height:20px;'></iframe></span> <span class='entry-footer-separator'>|</span> <span class='entry-footer-links-plusone'><g:plusone size='medium' href='http://sethgodin.typepad.com/seths_blog/2016/04/your-money-and-your-future.html' count='true'></g:plusone></span> <span class='entry-footer-separator'>|</span> <script type='IN/Share' data-counter='right' data-url='http://sethgodin.typepad.com/seths_blog/2016/04/your-money-and-your-future.html'></script> <span class='entry-footer-separator'>|</span> <span class='entry-footer-links-facebook_like'><iframe src='http://www.facebook.com/plugins/like.php?href=http://sethgodin.typepad.com/seths_blog/2016/04/your-money-and-your-future.html&amp;send=false&amp;layout=button_count&amp;show_faces=false&amp;action=like&amp;colorscheme=light&amp;font=arial&amp;height=21' scrolling='no' frameborder='0' style='border:none; overflow:visible; height:21px;' allowTransparency='true'></iframe></span>
</p></div>
				
			</div><!-- .entry-footer -->
			
			</div><!-- .entry -->
			
			
			
			<!-- this displays your posts, without any post you've marked as 'featured' -->
			
			<!--MTDateHeader>
				<h2 class='date-header'>April 29, 2016</h2>
			</MTDateHeader-->
			
			<div class='entry' id='entry-6a00d83451b31569e201b7c84c9e97970b'>
			
			<h3 class='entry-header '>
				<a href='http://sethgodin.typepad.com/seths_blog/2016/04/closing-the-gate.html'>Closing the gate</a>
			</h3><!-- .entry-header -->
			
			<div class='entry-content'>
			
			<div class='entry-body'>
				<p>Sooner or later, tribes begin to exclude interested but unaffiliated newcomers.</p>
<p>It happens to religious sects, to <a href='http://www.theguardian.com/travel/video/2015/may/18/california-surf-wars-lunada-bay-localism-video' target='_blank'>surfers</a> and to online communities as well. Nascent groups with open arms become mature groups too set in their ways to evangelize and grow their membership, too stuck to engage, change&#0160;and thrive.</p>
<p>So much easier to turn someone away than it is to patiently engage with them, the way you were welcomed when you were in their shoes.</p>
<p>There are two reasons for this:</p>
<ol>
<li>It&#39;s tiresome and boring to keep breaking in newbies. <a href='https://en.wikipedia.org/wiki/Eternal_September' target='_blank'>Eternal September</a>, the never-ending stream of repetitive questions and mistakes can wear out even the most committed host. Your IT person wasn&#39;t born grouchy--it just happens.</li>
<li>It&#39;s threatening to the existing power structure. New voices want new procedures and fresh leadership.</li>
</ol>
<p>And so, Wikipedia has transformed itself into a club that&#39;s not particularly interested in welcoming new editors.</p>
<p>And the social club down the street has a membership with an average age of 77.</p>
<p>And&#0160;companies that used to grow by absorbing talent via acquisitions, cease to do so.</p>
<p>This cycle isn&#39;t inevitable, but it takes ever more effort to overcome our inertia.</p>
<p>Even if it happens gradually, the choice to not fight this inertia is still a choice. And while&#0160;closing the gate can ensure stability and the status quo (for now), it&#0160;rarely leads to growth, and ultimately leads to decline.</p>
<p>[Some questions to ponder...]</p>
<p>Do outsiders get the benefit of the doubt?</p>
<p>Do we make it easy for outsiders to become insiders?</p>
<p>Is there a clear and well-lit path to do so?</p>
<p>When we tell someone new, &quot;that not how we do things around here,&quot; do we also encourage them to learn the other way and to try again?</p>
<p>Are we even capable of explaining the status quo, or is the way we do things set merely because we forgot that we could do it better?</p>
<p>Is a day without emotional or organizational growth a good day?</p>
			</div><!-- .entry-body -->
			
			
			
			</div><!-- .entry-content -->
			
			<div class='entry-footer'>
				<p class='entry-footer-info'>
			
				Posted by <a href='http://profile.typepad.com/sethgodin'>Seth Godin</a> on April 29, 2016
				
							
							
				<span class='separator'>|</span>
				
				<span class='entry-footer-permalink'><a href='http://sethgodin.typepad.com/seths_blog/2016/04/closing-the-gate.html'>Permalink</a></span>
				
				<!-- comment count -->
				
			
				
			
				</p><!-- .entry-footer-info -->

				
				
				<!-- post footer links -->
				<script src='http://platform.linkedin.com/in.js' type='text/javascript'></script>
<div class='share-counts'>
<p>
<span class='entry-footer-links-twitter'><iframe allowtransparency='true' frameborder='0' scrolling='no' src='http://platform.twitter.com/widgets/tweet_button.html?url=http://sethgodin.typepad.com/seths_blog/2016/04/closing-the-gate.html&text=Closing the gate&count=1' style='width:110px; height:20px;'></iframe></span> <span class='entry-footer-separator'>|</span> <span class='entry-footer-links-plusone'><g:plusone size='medium' href='http://sethgodin.typepad.com/seths_blog/2016/04/closing-the-gate.html' count='true'></g:plusone></span> <span class='entry-footer-separator'>|</span> <script type='IN/Share' data-counter='right' data-url='http://sethgodin.typepad.com/seths_blog/2016/04/closing-the-gate.html'></script> <span class='entry-footer-separator'>|</span> <span class='entry-footer-links-facebook_like'><iframe src='http://www.facebook.com/plugins/like.php?href=http://sethgodin.typepad.com/seths_blog/2016/04/closing-the-gate.html&amp;send=false&amp;layout=button_count&amp;show_faces=false&amp;action=like&amp;colorscheme=light&amp;font=arial&amp;height=21' scrolling='no' frameborder='0' style='border:none; overflow:visible; height:21px;' allowTransparency='true'></iframe></span>
</p></div>
				
			</div><!-- .entry-footer -->
			
			</div><!-- .entry -->
			
			
			
			<!-- this displays your posts, without any post you've marked as 'featured' -->
			
			<!--MTDateHeader>
				<h2 class='date-header'>April 28, 2016</h2>
			</MTDateHeader-->
			
			<div class='entry' id='entry-6a00d83451b31569e201b7c84b2ab6970b'>
			
			<h3 class='entry-header '>
				<a href='http://sethgodin.typepad.com/seths_blog/2016/04/transformation-tourism.html'>Transformation tourism</a>
			</h3><!-- .entry-header -->
			
			<div class='entry-content'>
			
			<div class='entry-body'>
				<p>&quot;I bought the diet book, but ate my usual foods.&quot;</p>
<p>&quot;I filled the prescription, but didn&#39;t take the meds.&quot;</p>
<p>&quot;I took the course... well, I watched the videos... but I didn&#39;t do the exercises in writing.&quot;</p>
<p>Merely looking at something almost never causes change. Tourism is fun, but rarely transformative.</p>
<p>If it was easy, you would have&#0160;already achieved the change you seek.</p>
<p>Change comes from new habits, from acting as if, from experiencing the inevitable discomfort of becoming.</p>
			</div><!-- .entry-body -->
			
			
			
			</div><!-- .entry-content -->
			
			<div class='entry-footer'>
				<p class='entry-footer-info'>
			
				Posted by <a href='http://profile.typepad.com/sethgodin'>Seth Godin</a> on April 28, 2016
				
							
							
				<span class='separator'>|</span>
				
				<span class='entry-footer-permalink'><a href='http://sethgodin.typepad.com/seths_blog/2016/04/transformation-tourism.html'>Permalink</a></span>
				
				<!-- comment count -->
				
			
				
			
				</p><!-- .entry-footer-info -->

				
				
				<!-- post footer links -->
				<script src='http://platform.linkedin.com/in.js' type='text/javascript'></script>
<div class='share-counts'>
<p>
<span class='entry-footer-links-twitter'><iframe allowtransparency='true' frameborder='0' scrolling='no' src='http://platform.twitter.com/widgets/tweet_button.html?url=http://sethgodin.typepad.com/seths_blog/2016/04/transformation-tourism.html&text=Transformation tourism&count=1' style='width:110px; height:20px;'></iframe></span> <span class='entry-footer-separator'>|</span> <span class='entry-footer-links-plusone'><g:plusone size='medium' href='http://sethgodin.typepad.com/seths_blog/2016/04/transformation-tourism.html' count='true'></g:plusone></span> <span class='entry-footer-separator'>|</span> <script type='IN/Share' data-counter='right' data-url='http://sethgodin.typepad.com/seths_blog/2016/04/transformation-tourism.html'></script> <span class='entry-footer-separator'>|</span> <span class='entry-footer-links-facebook_like'><iframe src='http://www.facebook.com/plugins/like.php?href=http://sethgodin.typepad.com/seths_blog/2016/04/transformation-tourism.html&amp;send=false&amp;layout=button_count&amp;show_faces=false&amp;action=like&amp;colorscheme=light&amp;font=arial&amp;height=21' scrolling='no' frameborder='0' style='border:none; overflow:visible; height:21px;' allowTransparency='true'></iframe></span>
</p></div>
				
			</div><!-- .entry-footer -->
			
			</div><!-- .entry -->
			
			
			
			<!-- this displays your posts, without any post you've marked as 'featured' -->
			
			<!--MTDateHeader>
				<h2 class='date-header'>April 27, 2016</h2>
			</MTDateHeader-->
			
			<div class='entry' id='entry-6a00d83451b31569e201b7c7a27e87970b'>
			
			<h3 class='entry-header '>
				<a href='http://sethgodin.typepad.com/seths_blog/2016/04/just-a-little-more.html'>Just a little more</a>
			</h3><!-- .entry-header -->
			
			<div class='entry-content'>
			
			<div class='entry-body'>
				<p>It&#39;s often about asking, not about what&#39;s needed.</p>
<p>Years ago, when I lived in California, I&#39;d go to the grocery store nearly every day. I usually paid by check. Each time, the clerk would ask me for my phone number and then write it on the check.</p>
<p>When I ran out of checks, I decided to be clever and had my phone number printed on them. You guessed it, without missing a beat, that same clerk started asking me for my driver&#39;s license number (and yes, I did it one more time, and we moved on to my social security number).</p>
<p>The information wasn&#39;t the point. It was the asking, the time taken to look closely at the document.</p>
<p>It&#39;s tempting to listen to our customers (&quot;why aren&#39;t there warm nuts in first class?&quot;) and then add the features they request. But often, you&#39;ll find that these very same customers are asking for something else. Maybe they don&#39;t actually want a discount, just the knowledge that they tried to get one.</p>
<p>What&#39;s really happening here is that people are seeking the edges, trying to find something that gets a reaction, a point of failure, proof that your patience, your largesse or your menu isn&#39;t infinite. Get patient with your toddler, and you might discover your toddler starts to seek a new way to get your attention. Give that investigating committee what they&#39;re asking, and they&#39;ll ask for something else.</p>
<p>They&#39;re not looking for one more thing, they&#39;re looking for a &#39;no&#39;, for acknowledgment that they reached the edge. That&#39;s precisely what they&#39;re seeking, and you&#39;re quite able to offer them that edge of finiteness.</p>
<p>Sometimes, &quot;no, I&#39;m sorry, we can&#39;t do that,&quot; is a feature.</p>
			</div><!-- .entry-body -->
			
			
			
			</div><!-- .entry-content -->
			
			<div class='entry-footer'>
				<p class='entry-footer-info'>
			
				Posted by <a href='http://profile.typepad.com/sethgodin'>Seth Godin</a> on April 27, 2016
				
							
							
				<span class='separator'>|</span>
				
				<span class='entry-footer-permalink'><a href='http://sethgodin.typepad.com/seths_blog/2016/04/just-a-little-more.html'>Permalink</a></span>
				
				<!-- comment count -->
				
			
				
			
				</p><!-- .entry-footer-info -->

				
				
				<!-- post footer links -->
				<script src='http://platform.linkedin.com/in.js' type='text/javascript'></script>
<div class='share-counts'>
<p>
<span class='entry-footer-links-twitter'><iframe allowtransparency='true' frameborder='0' scrolling='no' src='http://platform.twitter.com/widgets/tweet_button.html?url=http://sethgodin.typepad.com/seths_blog/2016/04/just-a-little-more.html&text=Just a little more&count=1' style='width:110px; height:20px;'></iframe></span> <span class='entry-footer-separator'>|</span> <span class='entry-footer-links-plusone'><g:plusone size='medium' href='http://sethgodin.typepad.com/seths_blog/2016/04/just-a-little-more.html' count='true'></g:plusone></span> <span class='entry-footer-separator'>|</span> <script type='IN/Share' data-counter='right' data-url='http://sethgodin.typepad.com/seths_blog/2016/04/just-a-little-more.html'></script> <span class='entry-footer-separator'>|</span> <span class='entry-footer-links-facebook_like'><iframe src='http://www.facebook.com/plugins/like.php?href=http://sethgodin.typepad.com/seths_blog/2016/04/just-a-little-more.html&amp;send=false&amp;layout=button_count&amp;show_faces=false&amp;action=like&amp;colorscheme=light&amp;font=arial&amp;height=21' scrolling='no' frameborder='0' style='border:none; overflow:visible; height:21px;' allowTransparency='true'></iframe></span>
</p></div>
				
			</div><!-- .entry-footer -->
			
			</div><!-- .entry -->
			
			
			
			<!-- this displays your posts, without any post you've marked as 'featured' -->
			
			<!--MTDateHeader>
				<h2 class='date-header'>April 26, 2016</h2>
			</MTDateHeader-->
			
			<div class='entry' id='entry-6a00d83451b31569e201bb08e0d53f970d'>
			
			<h3 class='entry-header '>
				<a href='http://sethgodin.typepad.com/seths_blog/2016/04/perfect-could-be-better.html'>Perfect; could be better</a>
			</h3><!-- .entry-header -->
			
			<div class='entry-content'>
			
			<div class='entry-body'>
				<p>When we run a new session&#0160;of the <a href='http://www.altmba.com' target='_blank'>altMBA</a>, we ask each student to write a short bio and submit a picture.</p>
<p>A week later, we share the nicely laid out PDF with&#0160;the extraordinary class that has been assembled and then give people a week to update their bio for mistakes, etc.</p>
<p>Inevitably, the bios (and the photos) get better. A lot better.</p>
<p>It&#39;s not because people didn&#39;t try the first time. It&#39;s because being surrounded by people on the same journey as you causes you to level up.</p>
<p>Your path&#0160;forward&#0160;is pretty simple: Decide on your journey and find some people who will cause you to level up.</p>
<p>There are only two sessions left in 2016 for the altMBA, then we&#39;re done for the year. Check out the&#0160;<a href='https://altmba.com/apply' target='_blank'>new application here.</a></p>
<p>&#0160;</p>
<p>If you&#39;re curious as to what we teach, here is some feedback from our alumni:</p>
<p>altMBA helped remind me that you are never too busy to do work that truly matters. &#0160;Clarissa Finks, altMBA3, <a href='http://www.burton.com/' target='_blank'>Burton Snowboards</a></p>
<p><span style='font-weight: 400;'>The altMBA taught me that there is no limit on empathy, or its positive and powerful application in business. &#0160;Matt Hill, altMBA3, <a href='http://www.nationalparksatnight.com/' target='_blank'>National Parks at Night</a></span></p>
<p><span style='font-weight: 400;'>Before the altMBA, I thought I was alone and that I needed other people’s help to succeed. After the altMBA, I know that I am not alone and that the right people will succeed with me. &#0160;Thejus Chakravarthy, altMBA4, <a href='http://korin.com/site/home.html' target='_blank'>Korin</a></span></p>
<p><span style='font-weight: 400;'>The altMBA taught me that it is&#0160;my turn to speak up about things that matter, that changing the world can&#0160;start with me. Heatherlee Nguyen, altMBA3, <a href='http://www.unitedhealthgroup.com/businesses/optum.aspx' target='_blank'>Optum</a> (UnitedHealth Group)</span></p>
<p><span style='font-weight: 400;'>The altMBA taught me that fear is not an excuse, and helped me learn how to silence my lizard brain. I am more confident, lighter, and confident in my ability to create the change in the world that I want to see. I was a dreamer, now I am a doer. &#0160;Alexa Rohn, AltMBA4, <a href='http://alexarohn.com/' target='_blank'><span style='font-weight: 400;'>alexarohn.com</span></a></span></p>
<p><span style='font-weight: 400;'>altMBA taught me that every decision, be it to ship, to sell, to connect or to understand another is rooted in emotion. The more you understand those emotions the better your product, pitch, friendship and leadership will resonate. &#0160; Alicia Johnson, altMBA4, <a href='http://sfdem.org/' target='_blank'>City of San Francisco Emergency Management</a></span></p>
<p><span style='font-weight: 400;'>The altMBA taught me that opportunity is a decision and it’s mine to make. &#0160;Derek W. Martin, altMBA1, <a href='http://www.tuba.co/' target='_blank'>tuba</a></span></p>
<p><span style='font-weight: 400;'><span style='font-weight: 400;'>altMBA taught me the value of real and thought-out feedback. &#0160;Cory Boehs, altMBA1, </span><a href='http://koolfoamllc.com/'><span style='font-weight: 400;'>Kool Foam</span></a></span></p>
<p>(Links for affiliation only).</p>
			</div><!-- .entry-body -->
			
			
			
			</div><!-- .entry-content -->
			
			<div class='entry-footer'>
				<p class='entry-footer-info'>
			
				Posted by <a href='http://profile.typepad.com/sethgodin'>Seth Godin</a> on April 26, 2016
				
							
							
				<span class='separator'>|</span>
				
				<span class='entry-footer-permalink'><a href='http://sethgodin.typepad.com/seths_blog/2016/04/perfect-could-be-better.html'>Permalink</a></span>
				
				<!-- comment count -->
				
			
				
			
				</p><!-- .entry-footer-info -->

				
				
				<!-- post footer links -->
				<script src='http://platform.linkedin.com/in.js' type='text/javascript'></script>
<div class='share-counts'>
<p>
<span class='entry-footer-links-twitter'><iframe allowtransparency='true' frameborder='0' scrolling='no' src='http://platform.twitter.com/widgets/tweet_button.html?url=http://sethgodin.typepad.com/seths_blog/2016/04/perfect-could-be-better.html&text=Perfect; could be better&count=1' style='width:110px; height:20px;'></iframe></span> <span class='entry-footer-separator'>|</span> <span class='entry-footer-links-plusone'><g:plusone size='medium' href='http://sethgodin.typepad.com/seths_blog/2016/04/perfect-could-be-better.html' count='true'></g:plusone></span> <span class='entry-footer-separator'>|</span> <script type='IN/Share' data-counter='right' data-url='http://sethgodin.typepad.com/seths_blog/2016/04/perfect-could-be-better.html'></script> <span class='entry-footer-separator'>|</span> <span class='entry-footer-links-facebook_like'><iframe src='http://www.facebook.com/plugins/like.php?href=http://sethgodin.typepad.com/seths_blog/2016/04/perfect-could-be-better.html&amp;send=false&amp;layout=button_count&amp;show_faces=false&amp;action=like&amp;colorscheme=light&amp;font=arial&amp;height=21' scrolling='no' frameborder='0' style='border:none; overflow:visible; height:21px;' allowTransparency='true'></iframe></span>
</p></div>
				
			</div><!-- .entry-footer -->
			
			</div><!-- .entry -->
			
			
			
			<!-- this displays your posts, without any post you've marked as 'featured' -->
			
			<!--MTDateHeader>
				<h2 class='date-header'>April 25, 2016</h2>
			</MTDateHeader-->
			
			<div class='entry' id='entry-6a00d83451b31569e201bb0840e9a2970d'>
			
			<h3 class='entry-header '>
				<a href='http://sethgodin.typepad.com/seths_blog/2016/04/the-tidal-wave-is-overrated.html'>The tidal wave is overrated</a>
			</h3><!-- .entry-header -->
			
			<div class='entry-content'>
			
			<div class='entry-body'>
				<p>Yes, it can lead to wholesale destruction, but it&#39;s the incessant (but much smaller) daily tidal force that moves all boats, worldwide.</p>
<p>And far more powerful than either is the incredible impact of seepage, of moisture, of the liquid that makes things grow.</p>
<p>Facebook and other legendary companies didn&#39;t get that way all at once, and neither will you.</p>
<p>We can definitely spend time worrying about/building the tsunami, but it&#39;s the drip, drip, drip that will change everything in the long run.</p>
			</div><!-- .entry-body -->
			
			
			
			</div><!-- .entry-content -->
			
			<div class='entry-footer'>
				<p class='entry-footer-info'>
			
				Posted by <a href='http://profile.typepad.com/sethgodin'>Seth Godin</a> on April 25, 2016
				
							
							
				<span class='separator'>|</span>
				
				<span class='entry-footer-permalink'><a href='http://sethgodin.typepad.com/seths_blog/2016/04/the-tidal-wave-is-overrated.html'>Permalink</a></span>
				
				<!-- comment count -->
				
			
				
			
				</p><!-- .entry-footer-info -->

				
				
				<!-- post footer links -->
				<script src='http://platform.linkedin.com/in.js' type='text/javascript'></script>
<div class='share-counts'>
<p>
<span class='entry-footer-links-twitter'><iframe allowtransparency='true' frameborder='0' scrolling='no' src='http://platform.twitter.com/widgets/tweet_button.html?url=http://sethgodin.typepad.com/seths_blog/2016/04/the-tidal-wave-is-overrated.html&text=The tidal wave is overrated&count=1' style='width:110px; height:20px;'></iframe></span> <span class='entry-footer-separator'>|</span> <span class='entry-footer-links-plusone'><g:plusone size='medium' href='http://sethgodin.typepad.com/seths_blog/2016/04/the-tidal-wave-is-overrated.html' count='true'></g:plusone></span> <span class='entry-footer-separator'>|</span> <script type='IN/Share' data-counter='right' data-url='http://sethgodin.typepad.com/seths_blog/2016/04/the-tidal-wave-is-overrated.html'></script> <span class='entry-footer-separator'>|</span> <span class='entry-footer-links-facebook_like'><iframe src='http://www.facebook.com/plugins/like.php?href=http://sethgodin.typepad.com/seths_blog/2016/04/the-tidal-wave-is-overrated.html&amp;send=false&amp;layout=button_count&amp;show_faces=false&amp;action=like&amp;colorscheme=light&amp;font=arial&amp;height=21' scrolling='no' frameborder='0' style='border:none; overflow:visible; height:21px;' allowTransparency='true'></iframe></span>
</p></div>
				
			</div><!-- .entry-footer -->
			
			</div><!-- .entry -->
			
			
			
			<!-- this displays your posts, without any post you've marked as 'featured' -->
			
			<!--MTDateHeader>
				<h2 class='date-header'>April 24, 2016</h2>
			</MTDateHeader-->
			
			<div class='entry' id='entry-6a00d83451b31569e201b8d1c736a6970c'>
			
			<h3 class='entry-header '>
				<a href='http://sethgodin.typepad.com/seths_blog/2016/04/the-other-kind-of-power-move.html'>The other kind of power move</a>
			</h3><!-- .entry-header -->
			
			<div class='entry-content'>
			
			<div class='entry-body'>
				<p>In the common vernacular, a power move is something that gets done to you.&#016#0160;</p>
<p>The person with power demands an accommodation, or switches the venue, or has an admin call you instead of calling you himself. Someone with a resource who makes you jump a little higher before he shares it...</p>
<p>Little diva-like gestures to reinforce who has the upper hand.</p>
<p>But what about moves that are based on connection, or generosity, or kindness?</p>
<p>Those take real power.</p>
			</div><!-- .entry-body -->
			
			
			
			</div><!-- .entry-content -->
			
			<div class='entry-footer'>
				<p class='entry-footer-info'>
			
				Posted by <a href='http://profile.typepad.com/sethgodin'>Seth Godin</a> on April 24, 2016
				
							
							
				<span class='separator'>|</span>
				
				<span class='entry-footer-permalink'><a href='http://sethgodin.typepad.com/seths_blog/2016/04/the-other-kind-of-power-move.html'>Permalink</a></span>
				
				<!-- comment count -->
				
			
				
			
				</p><!-- .entry-footer-info -->

				
				
				<!-- post footer links -->
				<script src='http://platform.linkedin.com/in.js' type='text/javascript'></script>
<div class='share-counts'>
<p>
<span class='entry-footer-links-twitter'><iframe allowtransparency='true' frameborder='0' scrolling='no' src='http://platform.twitter.com/widgets/tweet_button.html?url=http://sethgodin.typepad.com/seths_blog/2016/04/the-other-kind-of-power-move.html&text=The other kind of power move&count=1' style='width:110px; height:20px;'></iframe></span> <span class='entry-footer-separator'>|</span> <span class='entry-footer-links-plusone'><g:plusone size='medium' href='http://sethgodin.typepad.com/seths_blog/2016/04/the-other-kind-of-power-move.html' count='true'></g:plusone></span> <span class='entry-footer-separator'>|</span> <script type='IN/Share' data-counter='right' data-url='http://sethgodin.typepad.com/seths_blog/2016/04/the-other-kind-of-power-move.html'></script> <span class='entry-footer-separator'>|</span> <span class='entry-footer-links-facebook_like'><iframe src='http://www.facebook.com/plugins/like.php?href=http://sethgodin.typepad.com/seths_blog/2016/04/the-other-kind-of-power-move.html&amp;send=false&amp;layout=button_count&amp;show_faces=false&amp;action=like&amp;colorscheme=light&amp;font=arial&amp;height=21' scrolling='no' frameborder='0' style='border:none; overflow:visible; height:21px;' allowTransparency='true'></iframe></span>
</p></div>
				
			</div><!-- .entry-footer -->
			
			</div><!-- .entry -->
			
			
			
			<!-- this displays your posts, without any post you've marked as 'featured' -->
			
			<!--MTDateHeader>
				<h2 class='date-header'>April 23, 2016</h2>
			</MTDateHeader-->
			
			<div class='entry' id='entry-6a00d83451b31569e201bb07d6c72c970d'>
			
			<h3 class='entry-header '>
				<a href='http://sethgodin.typepad.com/seths_blog/2016/04/supply-and-demand.html'>Supply and demand</a>
			</h3><!-- .entry-header -->
			
			<div class='entry-content'>
			
			<div class='entry-body'>
				<p>Just because you have a supply (a skill, an inventory, a location) that doesn&#39;t necessarily mean you are entitled to demand.</p>
<p>The market decides what it wants. You can do your best to influence that choice, but it&#39;s never (alas) based on what you happen to already have.</p>
<p>There&#39;s a reason that garage sale prices tend to be pretty low.</p>
<p>We can get pretty self-involved on supply, forgetting that nothing works without demand.</p>
			</div><!-- .entry-body -->
			
			
			
			</div><!-- .entry-content -->
			
			<div class='entry-footer'>
				<p class='entry-footer-info'>
			
				Posted by <a href='http://profile.typepad.com/sethgodin'>Seth Godin</a> on April 23, 2016
				
							
							
				<span class='separator'>|</span>
				
				<span class='entry-footer-permalink'><a href='http://sethgodin.typepad.com/seths_blog/2016/04/supply-and-demand.html'>Permalink</a></span>
				
				<!-- comment count -->
				
			
				
			
				</p><!-- .entry-footer-info -->

				
				
				<!-- post footer links -->
				<script src='http://platform.linkedin.com/in.js' type='text/javascript'></script>
<div class='share-counts'>
<p>
<span class='entry-footer-links-twitter'><iframe allowtransparency='true' frameborder='0' scrolling='no' src='http://platform.twitter.com/widgets/tweet_button.html?url=http://sethgodin.typepad.com/seths_blog/2016/04/supply-and-demand.html&text=Supply and demand&count=1' style='width:110px; height:20px;'></iframe></span> <span class='entry-footer-separator'>|</span> <span class='entry-footer-links-plusone'><g:plusone size='medium' href='http://sethgodin.typepad.com/seths_blog/2016/04/supply-and-demand.html' count='true'></g:plusone></span> <span class='entry-footer-separator'>|</span> <script type='IN/Share' data-counter='right' data-url='http://sethgodin.typepad.com/seths_blog/2016/04/supply-and-demand.html'></script> <span class='entry-footer-separator'>|</span> <span class='entry-footer-links-facebook_like'><iframe src='http://www.facebook.com/plugins/like.php?href=http://sethgodin.typepad.com/seths_blog/2016/04/supply-and-demand.html&amp;send=false&amp;layout=button_count&amp;show_faces=false&amp;action=like&amp;colorscheme=light&amp;font=arial&amp;height=21' scrolling='no' frameborder='0' style='border:none; overflow:visible; height:21px;' allowTransparency='true'></iframe></span>
</p></div>
				
			</div><!-- .entry-footer -->
			
			</div><!-- .entry -->
			
			
			
			<!-- this displays your posts, without any post you've marked as 'featured' -->
			
			<!--MTDateHeader>
				<h2 class='date-header'>April 22, 2016</h2>
			</MTDateHeader-->
			
			<div class='entry' id='entry-6a00d83451b31569e201b8d1786357970c'>
			
			<h3 class='entry-header '>
				<a href='http://sethgodin.typepad.com/seths_blog/2016/04/turning-paradoxes-into-problems.html'>Turning paradoxes into problems</a>
			</h3><!-- .entry-header -->
			
			<div class='entry-content'>
			
			<div class='entry-body'>
				<p>A problem&#0160;is open to a solution. That what makes it a problem.</p>
<p>A paradox, on the other hand, is gated by boundaries that make a solution impossible.</p>
<p>If you&#39;ve been working on a situation, chewing on it, throwing everything you&#39;ve got at it, it might not be a problem at&#0160;all. You may have invented a paradox, creating so many limits that you&#39;ll never get anywhere.</p>
<p>It makes no sense to work on a paradox. Drop it and move on. Even better, figure out which boundaries to remove and turn it into a problem instead.</p>
<p>Two examples: Building a worldwide limo fleet is impossible, a paradox that requires too much money and too much time--by the time you raised enough money and hired enough supervisors, you&#39;d never be able to charge enough to earn it back. But once you ease the boundary of, &quot;if you own a transport service, you must own the cars and hire the drivers,&quot; the idea of building a network is merely a problem.</p>
<p>Another more general one: Making significant forward motion without offending anyone or exposing yourself to fear is a paradox. But once you&#39;re willing to relax those boundaries, it becomes a problem, one with side effects you&#39;re willing to live with...</p>
			</div><!-- .entry-body -->
			
			
			
			</div><!-- .entry-content -->
			
			<div class='entry-footer'>
				<p class='entry-footer-info'>
			
				Posted by <a href='http://profile.typepad.com/sethgodin'>Seth Godin</a> on April 22, 2016
				
							
							
				<span class='separator'>|</span>
				
				<span class='entry-footer-permalink'><a href='http://sethgodin.typepad.com/seths_blog/2016/04/turning-paradoxes-into-problems.html'>Permalink</a></span>
				
				<!-- comment count -->
				
			
				
			
				</p><!-- .entry-footer-info -->

				
				
				<!-- post footer links -->
				<script src='http://platform.linkedin.com/in.js' type='text/javascript'></script>
<div class='share-counts'>
<p>
<span class='entry-footer-links-twitter'><iframe allowtransparency='true' frameborder='0' scrolling='no' src='http://platform.twitter.com/widgets/tweet_button.html?url=http://sethgodin.typepad.com/seths_blog/2016/04/turning-paradoxes-into-problems.html&text=Turning paradoxes into problems&count=1' style='width:110px; height:20px;'></iframe></span> <span class='entry-footer-separator'>|</span> <span class='entry-footer-links-plusone'><g:plusone size='medium' href='http://sethgodin.typepad.com/seths_blog/2016/04/turning-paradoxes-into-problems.html' count='true'></g:plusone></span> <span class='entry-footer-separator'>|</span> <script type='IN/Share' data-counter='right' data-url='http://sethgodin.typepad.com/seths_blog/2016/04/turning-paradoxes-into-problems.html'></script> <span class='entry-footer-separator'>|</span> <span class='entry-footer-links-facebook_like'><iframe src='http://www.facebook.com/plugins/like.php?href=http://sethgodin.typepad.com/seths_blog/2016/04/turning-paradoxes-into-problems.html&amp;send=false&amp;layout=button_count&amp;show_faces=false&amp;action=like&amp;colorscheme=light&amp;font=arial&amp;height=21' scrolling='no' frameborder='0' style='border:none; overflow:visible; height:21px;' allowTransparency='true'></iframe></span>
</p></div>
				
			</div><!-- .entry-footer -->
			
			</div><!-- .entry -->
			
			
			
			<!-- this displays your posts, without any post you've marked as 'featured' -->
			
			<!--MTDateHeader>
				<h2 class='date-header'>April 21, 2016</h2>
			</MTDateHeader-->
			
			<div class='entry' id='entry-6a00d83451b31569e201b7c72b8bb0970b'>
			
			<h3 class='entry-header '>
				<a href='http://sethgodin.typepad.com/seths_blog/2016/04/processing-feedback.html'>Processing feedback</a>
			</h3><!-- .entry-header -->
			
			<div class='entry-content'>
			
			<div class='entry-body'>
				<p>This&#0160;is one of the&#0160;most important untaught skills available to each of us.</p>
<p>Three times in a row, a salesperson is rejected by one prospect after another.</p>
<p>A customer complains to a company that its website is not working with her browser.</p>
<p>An editor rejects the manuscript from a first-time novelist...</p>
<p>What to do?</p>
<p>How do we deal with the troll who enjoys creating uncertainty? Or the person carrying around a bagful of pain that she needs to share? How do we differentiate between constructive, useful insight and the other kind? How do we decide which feedback is actually a clue about how our core audience feels, and which is a distraction, a shortcut on the road to mediocre banality?</p>
<p>If you listen to none of the feedback, you will learn nothing. If you listen to all of it, nothing will happen.</p>
<p>Like all life skills, there&#39;s not a glib answer.</p>
<p>But we can definitely ask the questions. And get better at the art of listening (and dismissing).&#0160;</p>
<p>The place to start is with two categories. The category of, &quot;I actively seek this sort of feedback out and listen to it and act on it.&quot; And the category of, &quot;I&#39;m not interested in hearing that.&quot; There is no room for a third category.</p>
			</div><!-- .entry-body -->
			
			
			
			</div><!-- .entry-content -->
			
			<div class='entry-footer'>
				<p class='entry-footer-info'>
			
				Posted by <a href='http://profile.typepad.com/sethgodin'>Seth Godin</a> on April 21, 2016
				
							
							
				<span class='separator'>|</span>
				
				<span class='entry-footer-permalink'><a href='http://sethgodin.typepad.com/seths_blog/2016/04/processing-feedback.html'>Permalink</a></span>
				
				<!-- comment count -->
				
			
				
			
				</p><!-- .entry-footer-info -->

				
				
				<!-- post footer links -->
				<script src='http://platform.linkedin.com/in.js' type='text/javascript'></script>
<div class='share-counts'>
<p>
<span class='entry-footer-links-twitter'><iframe allowtransparency='true' frameborder='0' scrolling='no' src='http://platform.twitter.com/widgets/tweet_button.html?url=http://sethgodin.typepad.com/seths_blog/2016/04/processing-feedback.html&text=Processing feedback&count=1' style='width:110px; height:20px;'></iframe></span> <span class='entry-footer-separator'>|</span> <span class='entry-footer-links-plusone'><g:plusone size='medium' href='http://sethgodin.typepad.com/seths_blog/2016/04/processing-feedback.html' count='true'></g:plusone></span> <span class='entry-footer-separator'>|</span> <script type='IN/Share' data-counter='right' data-url='http://sethgodin.typepad.com/seths_blog/2016/04/processing-feedback.html'></script> <span class='entry-footer-separator'>|</span> <span class='entry-footer-links-facebook_like'><iframe src='http://www.facebook.com/plugins/like.php?href=http://sethgodin.typepad.com/seths_blog/2016/04/processing-feedback.html&amp;send=false&amp;layout=button_count&amp;show_faces=false&amp;action=like&amp;colorscheme=light&amp;font=arial&amp;height=21' scrolling='no' frameborder='0' style='border:none; overflow:visible; height:21px;' allowTransparency='true'></iframe></span>
</p></div>
				
			</div><!-- .entry-footer -->
			
			</div><!-- .entry -->
			
			
			
			<!-- this displays your posts, without any post you've marked as 'featured' -->
			
			<!--MTDateHeader>
				<h2 class='date-header'>April 20, 2016</h2>
			</MTDateHeader-->
			
			<div class='entry' id='entry-6a00d83451b31569e201b8d1c8f706970c'>
			
			<h3 class='entry-header '>
				<a href='http://sethgodin.typepad.com/seths_blog/2016/04/numbers-and-the-magic-of-measuring-the-right-thing.html'>Numbers (and the magic of measuring the right thing)</a>
			</h3><!-- .entry-header -->
			
			<div class='entry-content'>
			
			<div class='entry-body'>
				<p>What you measure usually gets paid attention to, and what you pay attention to, usually gets better.</p>
<p>Numbers supercharge measurement, because numbers are easy to compare.</p>
<p>Numbers make it difficult to hide.</p>
<p>And hence the problem.</p>
<p>Income is easy to measure, and so we fall into the trap that people who make more money are better, or happier, or somehow more worthy of <a href='https://medium.com/life-tips/here-are-two-things-worth-understanding-about-poverty-ff283f07b56b#.6hh2i7uae' target='_blank'>respect and dignity</a>.</p>
<p>Likes are easy to measure, so social media becomes a race to the bottom, where the panderer and the exhibitionist win.</p>
<p>Five star reviews are easy to measure, so creators feel the pressure to get more of them.</p>
<p>But wait!</p>
<p>What does it mean to &#39;win&#39;? Is maximizing the convenient number actually going to produce the impact and the outcome you wanted?</p>
<p>Is the most important work always the most popular? Does widespread acceptance translate into significant impact? Or even significant sales? Is the bestseller list also the bestbook list?</p>
<p>Who are these reviews from? Are they based on expectations (a marketing function) or are they based on the change you were trying to make? It turns out that great books and great movies get more than their fair share of lousy reviews--because popular items attract more users, and those users might not&#0160;be people you were&#0160;seeking to please.</p>
<p>Or consider graduation rates. The easiest way to make them go up is to lower standards. Or to get troublesome students to transfer to other institutions or even to get them arrested. When we lose track of what&#39;s important in our rush to keep track of what&#39;s measurable, we fail.</p>
<p>The right numbers matter. A hundred years ago, Henry Ford figured out how to build a car far cheaper than his competitors. He was selling the Model T for a fraction of&#0160;what it cost other companies to even <em>make</em> one of their cars. And so measuring the cost of manufacture became urgent and essential.</p>
<p>And farmers discovered the yield was the secret to their success, so tons&#0160;per acre became the most important thing to measure. Until people started keeping track of flavor, nutrition and side effects.</p>
<p>And then generals starting measuring body count...</p>
<p>When you measure the wrong thing, you get the wrong thing. Perhaps you can be precise in your measurement, but<em> precision is not significance.</em></p>
<p>On the other hand, when you are able to expose your work and your process to the right thing, to the metric that actually matters, good things happen.</p>
<p>We need to spend more time figuring out what to keep track of, and less time actually obsessing over the numbers that we are already measuring.</p>
			</div><!-- .entry-body -->
			
			
			
			</div><!-- .entry-content -->
			
			<div class='entry-footer'>
				<p class='entry-footer-info'>
			
				Posted by <a href='http://profile.typepad.com/sethgodin'>Seth Godin</a> on April 20, 2016
				
							
							
				<span class='separator'>|</span>
				
				<span class='entry-footer-permalink'><a href='http://sethgodin.typepad.com/seths_blog/2016/04/numbers-and-the-magic-of-measuring-the-right-thing.html'>Permalink</a></span>
				
				<!-- comment count -->
				
			
				
			
				</p><!-- .entry-footer-info -->

				
				
				<!-- post footer links -->
				<script src='http://platform.linkedin.com/in.js' type='text/javascript'></script>
<div class='share-counts'>
<p>
<span class='entry-footer-links-twitter'><iframe allowtransparency='true' frameborder='0' scrolling='no' src='http://platform.twitter.com/widgets/tweet_button.html?url=http://sethgodin.typepad.com/seths_blog/2016/04/numbers-and-the-magic-of-measuring-the-right-thing.html&text=Numbers (and the magic of measuring the right thing)&count=1' style='width:110px; height:20px;'></iframe></span> <span class='entry-footer-separator'>|</span> <span class='entry-footer-links-plusone'><g:plusone size='medium' href='http://sethgodin.typepad.com/seths_blog/2016/04/numbers-and-the-magic-of-measuring-the-right-thing.html' count='true'></g:plusone></span> <span class='entry-footer-separator'>|</span> <script type='IN/Share' data-counter='right' data-url='http://sethgodin.typepad.com/seths_blog/2016/04/numbers-and-the-magic-of-measuring-the-right-thing.html'></script> <span class='entry-footer-separator'>|</span> <span class='entry-footer-links-facebook_like'><iframe src='http://www.facebook.com/plugins/like.php?href=http://sethgodin.typepad.com/seths_blog/2016/04/numbers-and-the-magic-of-measuring-the-right-thing.html&amp;send=false&amp;layout=button_count&amp;show_faces=false&amp;action=like&amp;colorscheme=light&amp;font=arial&amp;height=21' scrolling='no' frameborder='0' style='border:none; overflow:visible; height:21px;' allowTransparency='true'></iframe></span>
</p></div>
				
			</div><!-- .entry-footer -->
			
			</div><!-- .entry -->
			
			
			
			<!-- this displays your posts, without any post you've marked as 'featured' -->
			
			<!--MTDateHeader>
				<h2 class='date-header'>April 19, 2016</h2>
			</MTDateHeader-->
			
			<div class='entry' id='entry-6a00d83451b31569e201b7c83d21c8970b'>
			
			<h3 class='entry-header '>
				<a href='http://sethgodin.typepad.com/seths_blog/2016/04/abstaining.html'>Abstaining</a>
			</h3><!-- .entry-header -->
			
			<div class='entry-content'>
			
			<div class='entry-body'>
				<p>Not voting leads to an outcome as much as voting does. You&#39;re still responsible, even if you didn&#39;t actively participate.</p>
<p>In any situation, not stating your opinion allows things to move forward. Silence is not nothing, it is still an action.&#0160;</p>
<p>No sense hiding, from yourself or anyone else.</p>
			</div><!-- .entry-body -->
			
			
			
			</div><!-- .entry-content -->
			
			<div class='entry-footer'>
				<p class='entry-footer-info'>
			
				Posted by <a href='http://profile.typepad.com/sethgodin'>Seth Godin</a> on April 19, 2016
				
							
							
				<span class='separator'>|</span>
				
				<span class='entry-footer-permalink'><a href='http://sethgodin.typepad.com/seths_blog/2016/04/abstaining.html'>Permalink</a></span>
				
				<!-- comment count -->
				
			
				
			
				</p><!-- .entry-footer-info -->

				
				
				<!-- post footer links -->
				<script src='http://platform.linkedin.com/in.js' type='text/javascript'></script>
<div class='share-counts'>
<p>
<span class='entry-footer-links-twitter'><iframe allowtransparency='true' frameborder='0' scrolling='no' src='http://platform.twitter.com/widgets/tweet_button.html?url=http://sethgodin.typepad.com/seths_blog/2016/04/abstaining.html&text=Abstaining&count=1' style='width:110px; height:20px;'></iframe></span> <span class='entry-footer-separator'>|</span> <span class='entry-footer-links-plusone'><g:plusone size='medium' href='http://sethgodin.typepad.com/seths_blog/2016/04/abstaining.html' count='true'></g:plusone></span> <span class='entry-footer-separator'>|</span> <script type='IN/Share' data-counter='right' data-url='http://sethgodin.typepad.com/seths_blog/2016/04/abstaining.html'></script> <span class='entry-footer-separator'>|</span> <span class='entry-footer-links-facebook_like'><iframe src='http://www.facebook.com/plugins/like.php?href=http://sethgodin.typepad.com/seths_blog/2016/04/abstaining.html&amp;send=false&amp;layout=button_count&amp;show_faces=false&amp;action=like&amp;colorscheme=light&amp;font=arial&amp;height=21' scrolling='no' frameborder='0' style='border:none; overflow:visible; height:21px;' allowTransparency='true'></iframe></span>
</p></div>
				
			</div><!-- .entry-footer -->
			
			</div><!-- .entry -->
			
			
			
			<!-- this displays your posts, without any post you've marked as 'featured' -->
			
			<!--MTDateHeader>
				<h2 class='date-header'>April 18, 2016</h2>
			</MTDateHeader-->
			
			<div class='entry' id='entry-6a00d83451b31569e201bb08df3d13970d'>
			
			<h3 class='entry-header '>
				<a href='http://sethgodin.typepad.com/seths_blog/2016/04/it-feels-risky.html'>It feels risky</a>
			</h3><!-- .entry-header -->
			
			<div class='entry-content'>
			
			<div class='entry-body'>
				<p>Risk and the appearance of risk aren&#39;t the same thing.</p>
<p>In fact, for most of us, they rarely overlap.</p>
<p>Realizing that there&#39;s a difference is the first step&#0160;in making better decisions.</p>
			</div><!-- .entry-body -->
			
			
			
			</div><!-- .entry-content -->
			
			<div class='entry-footer'>
				<p class='entry-footer-info'>
			
				Posted by <a href='http://profile.typepad.com/sethgodin'>Seth Godin</a> on April 18, 2016
				
							
							
				<span class='separator'>|</span>
				
				<span class='entry-footer-permalink'><a href='http://sethgodin.typepad.com/seths_blog/2016/04/it-feels-risky.html'>Permalink</a></span>
				
				<!-- comment count -->
				
			
				
			
				</p><!-- .entry-footer-info -->

				
				
				<!-- post footer links -->
				<script src='http://platform.linkedin.com/in.js' type='text/javascript'></script>
<div class='share-counts'>
<p>
<span class='entry-footer-links-twitter'><iframe allowtransparency='true' frameborder='0' scrolling='no' src='http://platform.twitter.com/widgets/tweet_button.html?url=http://sethgodin.typepad.com/seths_blog/2016/04/it-feels-risky.html&text=It feels risky&count=1' style='width:110px; height:20px;'></iframe></span> <span class='entry-footer-separator'>|</span> <span class='entry-footer-links-plusone'><g:plusone size='medium' href='http://sethgodin.typepad.com/seths_blog/2016/04/it-feels-risky.html' count='true'></g:plusone></span> <span class='entry-footer-separator'>|</span> <script type='IN/Share' data-counter='right' data-url='http://sethgodin.typepad.com/seths_blog/2016/04/it-feels-risky.html'></script> <span class='entry-footer-separator'>|</span> <span class='entry-footer-links-facebook_like'><iframe src='http://www.facebook.com/plugins/like.php?href=http://sethgodin.typepad.com/seths_blog/2016/04/it-feels-risky.html&amp;send=false&amp;layout=button_count&amp;show_faces=false&amp;action=like&amp;colorscheme=light&amp;font=arial&amp;height=21' scrolling='no' frameborder='0' style='border:none; overflow:visible; height:21px;' allowTransparency='true'></iframe></span>
</p></div>
				
			</div><!-- .entry-footer -->
			
			</div><!-- .entry -->
			
			
			
			<!-- this displays your posts, without any post you've marked as 'featured' -->
			
			<!--MTDateHeader>
				<h2 class='date-header'>April 17, 2016</h2>
			</MTDateHeader-->
			
			<div class='entry' id='entry-6a00d83451b31569e201b7c79c48aa970b'>
			
			<h3 class='entry-header '>
				<a href='http://sethgodin.typepad.com/seths_blog/2016/04/awareness-trust-and-action.html'>Awareness, trust and action</a>
			</h3><!-- .entry-header -->
			
			<div class='entry-content'>
			
			<div class='entry-body'>
				<p>Marketing outreach (ads, PR, sponsorships, etc.) is not about one thing. It&#39;s about three things.</p>
<p><em>Awareness</em> is a simple ping: Oh, she&#39;s running for President. Oh, they just opened one in our neighborhood. Oh, they&#39;re having a sale.</p>
<p><em>Trust</em> is far more complicated. Trust comes from experience, from word of mouth, from actions noted. Trust, amazingly, also seems to come from awareness. &quot;As seen on TV&quot; is a perverse way to claim trust, but in fact, when people are more aware of what you do, it often seeps into a sort of trust.</p>
<p>And <em>action</em> is what happens when someone actually goes and votes, or buys something, or shows up, or talks about it. And action is as complex as trust. Action requires overcoming the status quo, action means that someone has dealt with the many fears that come with change and felt that fear and still done something.</p>
<p>Many people reading this are aware that they can buy a new mattress, and might believe it&#39;s worth the effort, but don&#39;t take action.</p>
<p>Many people reading this are aware that they can buy a tool, get some treatment, visit a foreign land, listen to a new recording... but action is the difficult part.</p>
<p>Action is quite rare. For most people, the story of &#39;later&#39; is seductive enough that it appears better&#0160;to wait instead of&#0160;leaping.</p>
<p>As a marketer, then, part of the challenge is figuring out which of the three elements you need the most help with, and then focus on that...</p>
			</div><!-- .entry-body -->
			
			
			
			</div><!-- .entry-content -->
			
			<div class='entry-footer'>
				<p class='entry-footer-info'>
			
				Posted by <a href='http://profile.typepad.com/sethgodin'>Seth Godin</a> on April 17, 2016
				
							
							
				<span class='separator'>|</span>
				
				<span class='entry-footer-permalink'><a href='http://sethgodin.typepad.com/seths_blog/2016/04/awareness-trust-and-action.html'>Permalink</a></span>
				
				<!-- comment count -->
				
			
				
			
				</p><!-- .entry-footer-info -->

				
				
				<!-- post footer links -->
				<script src='http://platform.linkedin.com/in.js' type='text/javascript'></script>
<div class='share-counts'>
<p>
<span class='entry-footer-links-twitter'><iframe allowtransparency='true' frameborder='0' scrolling='no' src='http://platform.twitter.com/widgets/tweet_button.html?url=http://sethgodin.typepad.com/seths_blog/2016/04/awareness-trust-and-action.html&text=Awareness, trust and action&count=1' style='width:110px; height:20px;'></iframe></span> <span class='entry-footer-separator'>|</span> <span class='entry-footer-links-plusone'><g:plusone size='medium' href='http://sethgodin.typepad.com/seths_blog/2016/04/awareness-trust-and-action.html' count='true'></g:plusone></span> <span class='entry-footer-separator'>|</span> <script type='IN/Share' data-counter='right' data-url='http://sethgodin.typepad.com/seths_blog/2016/04/awareness-trust-and-action.html'></script> <span class='entry-footer-separator'>|</span> <span class='entry-footer-links-facebook_like'><iframe src='http://www.facebook.com/plugins/like.php?href=http://sethgodin.typepad.com/seths_blog/2016/04/awareness-trust-and-action.html&amp;send=false&amp;layout=button_count&amp;show_faces=false&amp;action=like&amp;colorscheme=light&amp;font=arial&amp;height=21' scrolling='no' frameborder='0' style='border:none; overflow:visible; height:21px;' allowTransparency='true'></iframe></span>
</p></div>
				
			</div><!-- .entry-footer -->
			
			</div><!-- .entry -->
			
			
			
			<!-- this displays your posts, without any post you've marked as 'featured' -->
			
			<!--MTDateHeader>
				<h2 class='date-header'>April 16, 2016</h2>
			</MTDateHeader-->
			
			<div class='entry' id='entry-6a00d83451b31569e201b8d166eff3970c'>
			
			<h3 class='entry-header '>
				<a href='http://sethgodin.typepad.com/seths_blog/2016/04/i-am-not-a-brand.html'>I am not a brand</a>
			</h3><!-- .entry-header -->
			
			<div class='entry-content'>
			
			<div class='entry-body'>
				<p>You are not a brand.</p>
<p>You&#39;re a person.</p>
<p>A living, breathing, autonomous individual who doesn&#39;t seek to maximize ROI or long-term brand value.</p>
<p>You have choices. You have the ability to change your mind. You can tell the truth, see others for who they are and choose to make a difference.</p>
<p>Selling yourself as a brand sells you too cheap.</p>
<p>(Actually, if a brand is nothing but the promises made and kept and the expectations we have, then yes, I guess you are a brand. The modern kind, the brand where connection matters a lot more than ads or hype.)</p>
			</div><!-- .entry-body -->
			
			
			
			</div><!-- .entry-content -->
			
			<div class='entry-footer'>
				<p class='entry-footer-info'>
			
				Posted by <a href='http://profile.typepad.com/sethgodin'>Seth Godin</a> on April 16, 2016
				
							
							
				<span class='separator'>|</span>
				
				<span class='entry-footer-permalink'><a href='http://sethgodin.typepad.com/seths_blog/2016/04/i-am-not-a-brand.html'>Permalink</a></span>
				
				<!-- comment count -->
				
			
				
			
				</p><!-- .entry-footer-info -->

				
				
				<!-- post footer links -->
				<script src='http://platform.linkedin.com/in.js' type='text/javascript'></script>
<div class='share-counts'>
<p>
<span class='entry-footer-links-twitter'><iframe allowtransparency='true' frameborder='0' scrolling='no' src='http://platform.twitter.com/widgets/tweet_button.html?url=http://sethgodin.typepad.com/seths_blog/2016/04/i-am-not-a-brand.html&text=I am not a brand&count=1' style='width:110px; height:20px;'></iframe></span> <span class='entry-footer-separator'>|</span> <span class='entry-footer-links-plusone'><g:plusone size='medium' href='http://sethgodin.typepad.com/seths_blog/2016/04/i-am-not-a-brand.html' count='true'></g:plusone></span> <span class='entry-footer-separator'>|</span> <script type='IN/Share' data-counter='right' data-url='http://sethgodin.typepad.com/seths_blog/2016/04/i-am-not-a-brand.html'></script> <span class='entry-footer-separator'>|</span> <span class='entry-footer-links-facebook_like'><iframe src='http://www.facebook.com/plugins/like.php?href=http://sethgodin.typepad.com/seths_blog/2016/04/i-am-not-a-brand.html&amp;send=false&amp;layout=button_count&amp;show_faces=false&amp;action=like&amp;colorscheme=light&amp;font=arial&amp;height=21' scrolling='no' frameborder='0' style='border:none; overflow:visible; height:21px;' allowTransparency='true'></iframe></span>
</p></div>
				
			</div><!-- .entry-footer -->
			
			</div><!-- .entry -->
			
			
			
			<!-- this displays your posts, without any post you've marked as 'featured' -->
			
			<!--MTDateHeader>
				<h2 class='date-header'>April 15, 2016</h2>
			</MTDateHeader-->
			
			<div class='entry' id='entry-6a00d83451b31569e201bb08d609e7970d'>
			
			<h3 class='entry-header '>
				<a href='http://sethgodin.typepad.com/seths_blog/2016/04/apocalypse-soon.html'>Apocalypse soon</a>
			</h3><!-- .entry-header -->
			
			<div class='entry-content'>
			
			<div class='entry-body'>
				<p>It&#39;s a bug in our operating system, and one that&#39;s amplified by the media.</p>
<p>I&#39;m listening to a speech from ten years ago, twenty years ago, forty years ago... &quot;During these tough times... these tenuous times... these uncertain times...&quot; And we hear about the urgency of the day, the bomb shelters, the preppers with their water tanks, the hand wringing about the next threat to civilization.</p>
<p>At the same time that we live in the safest world that mankind has ever experienced. Fewer deaths per capita from all the things that we worry about.</p>
<p>Risky? Sure it is. Every moment for the last million years has been risky. The risk has moved from Og with a rock to the chronic degeneration of our climate, but it&#39;s clear that rehearsing and fretting and worrying about the issue of the day hasn&#39;t done a thing to actually make it go away. Instead, we amplify the fear, market the fear and spread the fear as a form of solace, of hiding from taking action, of sharing our fear in a vain attempt to ameliorate it.</p>
<p>When we get nostalgic for past eras, for their culture or economy or resources, it&#39;s interesting that we never seem to get nostalgic for their fears.</p>
			</div><!-- .entry-body -->
			
			
			
			</div><!-- .entry-content -->
			
			<div class='entry-footer'>
				<p class='entry-footer-info'>
			
				Posted by <a href='http://profile.typepad.com/sethgodin'>Seth Godin</a> on April 15, 2016
				
							
							
				<span class='separator'>|</span>
				
				<span class='entry-footer-permalink'><a href='http://sethgodin.typepad.com/seths_blog/2016/04/apocalypse-soon.html'>Permalink</a></span>
				
				<!-- comment count -->
				
			
				
			
				</p><!-- .entry-footer-info -->

				
				
				<!-- post footer links -->
				<script src='http://platform.linkedin.com/in.js' type='text/javascript'></script>
<div class='share-counts'>
<p>
<span class='entry-footer-links-twitter'><iframe allowtransparency='true' frameborder='0' scrolling='no' src='http://platform.twitter.com/widgets/tweet_button.html?url=http://sethgodin.typepad.com/seths_blog/2016/04/apocalypse-soon.html&text=Apocalypse soon&count=1' style='width:110px; height:20px;'></iframe></span> <span class='entry-footer-separator'>|</span> <span class='entry-footer-links-plusone'><g:plusone size='medium' href='http://sethgodin.typepad.com/seths_blog/2016/04/apocalypse-soon.html' count='true'></g:plusone></span> <span class='entry-footer-separator'>|</span> <script type='IN/Share' data-counter='right' data-url='http://sethgodin.typepad.com/seths_blog/2016/04/apocalypse-soon.html'></script> <span class='entry-footer-separator'>|</span> <span class='entry-footer-links-facebook_like'><iframe src='http://www.facebook.com/plugins/like.php?href=http://sethgodin.typepad.com/seths_blog/2016/04/apocalypse-soon.html&amp;send=false&amp;layout=button_count&amp;show_faces=false&amp;action=like&amp;colorscheme=light&amp;font=arial&amp;height=21' scrolling='no' frameborder='0' style='border:none; overflow:visible; height:21px;' allowTransparency='true'></iframe></span>
</p></div>
				
			</div><!-- .entry-footer -->
			
			</div><!-- .entry -->
			
			
			
			<!-- this displays your posts, without any post you've marked as 'featured' -->
			
			<!--MTDateHeader>
				<h2 class='date-header'>April 14, 2016</h2>
			</MTDateHeader-->
			
			<div class='entry' id='entry-6a00d83451b31569e201b7c8324de3970b'>
			
			<h3 class='entry-header '>
				<a href='http://sethgodin.typepad.com/seths_blog/2016/04/the-foggy-mirror.html'>The foggy mirror</a>
			</h3><!-- .entry-header -->
			
			<div class='entry-content'>
			
			<div class='entry-body'>
				<p>Most people can&#39;t resist a mirror. It makes the wait for an elevator more palatable, and we can&#39;t help checking--how do I look?</p>
<p>In many ways, though, this is futile, because&#0160;we can&#0160;never know how we&#0160;look through other people&#39;s eyes.</p>
<p>No one else has lived your life, heard all of your jokes, experienced your disappointments, listened to the noise in your head. As a result, no one else sees you (and your actions) quite the way you do.</p>
<p>And, to magnify the disconnect, every single person has their own narrative, so even when two people see you at the same time, they have different interpretations of what just happened, what was just said.</p>
<p>The same goes for brands and organizations. No one has experienced your brand or your product the way you have. They don&#39;t know about the compromises and choices that went into it. They don&#39;t understand the competitive pressures or the mis-steps either.</p>
<p>Even the best quality mirror tells you very little. It doesn&#39;t make a lot of sense to focus on this sort of grooming if you want to understand what customers or friends are going to see. Far better to watch what they do.</p>
<p>(But yes, you do have a little green thing stuck in your teeth).</p>
			</div><!-- .entry-body -->
			
			
			
			</div><!-- .entry-content -->
			
			<div class='entry-footer'>
				<p class='entry-footer-info'>
			
				Posted by <a href='http://profile.typepad.com/sethgodin'>Seth Godin</a> on April 14, 2016
				
							
							
				<span class='separator'>|</span>
				
				<span class='entry-footer-permalink'><a href='http://sethgodin.typepad.com/seths_blog/2016/04/the-foggy-mirror.html'>Permalink</a></span>
				
				<!-- comment count -->
				
			
				
			
				</p><!-- .entry-footer-info -->

				
				
				<!-- post footer links -->
				<script src='http://platform.linkedin.com/in.js' type='text/javascript'></script>
<div class='share-counts'>
<p>
<span class='entry-footer-links-twitter'><iframe allowtransparency='true' frameborder='0' scrolling='no' src='http://platform.twitter.com/widgets/tweet_button.html?url=http://sethgodin.typepad.com/seths_blog/2016/04/the-foggy-mirror.html&text=The foggy mirror&count=1' style='width:110px; height:20px;'></iframe></span> <span class='entry-footer-separator'>|</span> <span class='entry-footer-links-plusone'><g:plusone size='medium' href='http://sethgodin.typepad.com/seths_blog/2016/04/the-foggy-mirror.html' count='true'></g:plusone></span> <span class='entry-footer-separator'>|</span> <script type='IN/Share' data-counter='right' data-url='http://sethgodin.typepad.com/seths_blog/2016/04/the-foggy-mirror.html'></script> <span class='entry-footer-separator'>|</span> <span class='entry-footer-links-facebook_like'><iframe src='http://www.facebook.com/plugins/like.php?href=http://sethgodin.typepad.com/seths_blog/2016/04/the-foggy-mirror.html&amp;send=false&amp;layout=button_count&amp;show_faces=false&amp;action=like&amp;colorscheme=light&amp;font=arial&amp;height=21' scrolling='no' frameborder='0' style='border:none; overflow:visible; height:21px;' allowTransparency='true'></iframe></span>
</p></div>
				
			</div><!-- .entry-footer -->
			
			</div><!-- .entry -->
			
			
			
			<!-- this displays your posts, without any post you've marked as 'featured' -->
			
			<!--MTDateHeader>
				<h2 class='date-header'>April 13, 2016</h2>
			</MTDateHeader-->
			
			<div class='entry' id='entry-6a00d83451b31569e201bb08d821ce970d'>
			
			<h3 class='entry-header '>
				<a href='http://sethgodin.typepad.com/seths_blog/2016/04/finding-your-big-magic.html'>Finding your big magic</a>
			</h3><!-- .entry-header -->
			
			<div class='entry-content'>
			
			<div class='entry-body'>
				<p>Launching today, a new <a href='http://plusacumen.org/acumen-master-class-elizabeth-gilberts-creativity-workshop/' target='_blank'>master class</a>&#0160;from Elizabeth Gilbert.</p>
<p>Liz Gilbert is a gift. Hew new book <em><a href='http://smile.amazon.com/Big-Magic-Creative-Living-Beyond/dp/1594634718/ref=sr_1_1?ie=UTF8&amp;qid=1460551789&amp;sr=8-1&amp;keywords=big+magic+gilbert' target='_blank'>Big Magic</a></em> is a generous beam of light, a chance to shake off the ennui and fear that holds us back.</p>
<p>Last month, I was thrilled to be able to work with her on a new short&#0160;<a href='https://www.udemy.com/elizabeth-gilberts-creativity-workshop/?couponCode=%2BAcumen40' target='_blank'>Udemy</a>&#0160;course. It&#39;s launching today. The course runs on Udemy, and if you become part of + Acumen, it&#39;s only $29. I&#39;m grateful to her for her energy and insight, and for donating her time.</p>
<p>I think you&#39;ll be changed by the time you spend with her as well.</p>
<p>Liz has the extraordinary ability to help us find the genius within, to dig a bit deeper than we thought we could dig.</p>
<p>The single&#0160;four-minute riff in this course about hobbies and careers is worth the entire cost of the course. As I was standing in the corner of the room, feeling my energy and optimism rise, I realized I was witnessing something special.&#0160;</p>
<p>You can get the <a href='http://plusacumen.org/acumen-master-class-elizabeth-gilberts-creativity-workshop/' target='_blank'>discount</a> by joining + Acumen.</p>
<p>My leadership course which kicked off the series is still available. Details are&#0160;<a href='https://www.udemy.com/seth-godins-leadership-workshop/?couponCode=Acumen40'>here</a>&#0160;and the discount is&#0160;<a href='http://plusacumen.org/acumen-presents-seth-godins-leadership-workshop/' target='_blank'>here</a>.</p>
<p>Thank you for leaping, and for supporting this mission. So far, the long-form + Acumen courses have already engaged more than a quarter of a million people. This new series of mini-courses has, thanks to you, raised more than $125,000 to pay for the production of even more courses that will help people see a little farther and contribute a little more. &#0160;Worth noting that Jo-Ann Tan and Amy Ahearn at Acumen have made huge contributions to making this change a reality.&#0160;</p>
<p>Time to leap.</p>
			</div><!-- .entry-body -->
			
			
			
			</div><!-- .entry-content -->
			
			<div class='entry-footer'>
				<p class='entry-footer-info'>
			
				Posted by <a href='http://profile.typepad.com/sethgodin'>Seth Godin</a> on April 13, 2016
				
							
							
				<span class='separator'>|</span>
				
				<span class='entry-footer-permalink'><a href='http://sethgodin.typepad.com/seths_blog/2016/04/finding-your-big-magic.html'>Permalink</a></span>
				
				<!-- comment count -->
				
			
				
			
				</p><!-- .entry-footer-info -->

				
				
				<!-- post footer links -->
				<script src='http://platform.linkedin.com/in.js' type='text/javascript'></script>
<div class='share-counts'>
<p>
<span class='entry-footer-links-twitter'><iframe allowtransparency='true' frameborder='0' scrolling='no' src='http://platform.twitter.com/widgets/tweet_button.html?url=http://sethgodin.typepad.com/seths_blog/2016/04/finding-your-big-magic.html&text=Finding your big magic&count=1' style='width:110px; height:20px;'></iframe></span> <span class='entry-footer-separator'>|</span> <span class='entry-footer-links-plusone'><g:plusone size='medium' href='http://sethgodin.typepad.com/seths_blog/2016/04/finding-your-big-magic.html' count='true'></g:plusone></span> <span class='entry-footer-separator'>|</span> <script type='IN/Share' data-counter='right' data-url='http://sethgodin.typepad.com/seths_blog/2016/04/finding-your-big-magic.html'></script> <span class='entry-footer-separator'>|</span> <span class='entry-footer-links-facebook_like'><iframe src='http://www.facebook.com/plugins/like.php?href=http://sethgodin.typepad.com/seths_blog/2016/04/finding-your-big-magic.html&amp;send=false&amp;layout=button_count&amp;show_faces=false&amp;action=like&amp;colorscheme=light&amp;font=arial&amp;height=21' scrolling='no' frameborder='0' style='border:none; overflow:visible; height:21px;' allowTransparency='true'></iframe></span>
</p></div>
				
			</div><!-- .entry-footer -->
			
			</div><!-- .entry -->
			
			
			
			<!-- this displays your posts, without any post you've marked as 'featured' -->
			
			<!--MTDateHeader>
				<h2 class='date-header'>April 13, 2016</h2>
			</MTDateHeader-->
			
			<div class='entry' id='entry-6a00d83451b31569e201bb08d60847970d'>
			
			<h3 class='entry-header '>
				<a href='http://sethgodin.typepad.com/seths_blog/2016/04/sharpening-failure.html'>Sharpening failure</a>
			</h3><!-- .entry-header -->
			
			<div class='entry-content'>
			
			<div class='entry-body'>
				<p>Losing the election by ten votes or by a million--which is worse?</p>
<p>&quot;Missed it by <a href='https://www.youtube.com/watch?v=sBlhrTpi69E' target='_blank'>that much</a>,&quot; is a way to amplify how we feel when we don&#39;t succeed. So, when we miss the bus by just a few seconds, or finish&#0160;a math proof just behind the competition--we can beat ourselves up about this for years.</p>
<p>Much rarer, it seems, is the opposite. It&#39;s hard to find people still congratulating themselves after winning an election by just a few votes or making a plane by a step or two. Nice that it happened, but we ask what&#39;s next, where&#39;s the next crisis?</p>
<p>We have a name for someone who expects the worst in the future. Pessimism is a choice. But we don&#39;t seem to have a name for someone who describes the past with the same negative cast.</p>
<p>It&#39;s a dangerous trap, the regular reminders of how we&#39;ve failed, but how close we&#39;ve come to winning. It rarely leads us to prepare more, to be more adroit or dedicated. Instead, it&#39;s a form of hiding, a way to insulate ourselves from the next, apparently inevitable failure.</p>
<p>The universe is not laughing at us. It doesn&#39;t even know we&#0160;exist.&#0160;</p>
<p>Go ahead and celebrate the wins, then get back to work. Same for mourning the losses. All we can do is go forward.</p>
<p>&#0160;</p>
			</div><!-- .entry-body -->
			
			
			
			</div><!-- .entry-content -->
			
			<div class='entry-footer'>
				<p class='entry-footer-info'>
			
				Posted by <a href='http://profile.typepad.com/sethgodin'>Seth Godin</a> on April 13, 2016
				
							
							
				<span class='separator'>|</span>
				
				<span class='entry-footer-permalink'><a href='http://sethgodin.typepad.com/seths_blog/2016/04/sharpening-failure.html'>Permalink</a></span>
				
				<!-- comment count -->
				
			
				
			
				</p><!-- .entry-footer-info -->

				
				
				<!-- post footer links -->
				<script src='http://platform.linkedin.com/in.js' type='text/javascript'></script>
<div class='share-counts'>
<p>
<span class='entry-footer-links-twitter'><iframe allowtransparency='true' frameborder='0' scrolling='no' src='http://platform.twitter.com/widgets/tweet_button.html?url=http://sethgodin.typepad.com/seths_blog/2016/04/sharpening-failure.html&text=Sharpening failure&count=1' style='width:110px; height:20px;'></iframe></span> <span class='entry-footer-separator'>|</span> <span class='entry-footer-links-plusone'><g:plusone size='medium' href='http://sethgodin.typepad.com/seths_blog/2016/04/sharpening-failure.html' count='true'></g:plusone></span> <span class='entry-footer-separator'>|</span> <script type='IN/Share' data-counter='right' data-url='http://sethgodin.typepad.com/seths_blog/2016/04/sharpening-failure.html'></script> <span class='entry-footer-separator'>|</span> <span class='entry-footer-links-facebook_like'><iframe src='http://www.facebook.com/plugins/like.php?href=http://sethgodin.typepad.com/seths_blog/2016/04/sharpening-failure.html&amp;send=false&amp;layout=button_count&amp;show_faces=false&amp;action=like&amp;colorscheme=light&amp;font=arial&amp;height=21' scrolling='no' frameborder='0' style='border:none; overflow:visible; height:21px;' allowTransparency='true'></iframe></span>
</p></div>
				
			</div><!-- .entry-footer -->
			
			</div><!-- .entry -->
			
			
			
			<!-- this displays your posts, without any post you've marked as 'featured' -->
			
			<!--MTDateHeader>
				<h2 class='date-header'>April 12, 2016</h2>
			</MTDateHeader-->
			
			<div class='entry' id='entry-6a00d83451b31569e201b8d0b77b76970c'>
			
			<h3 class='entry-header '>
				<a href='http://sethgodin.typepad.com/seths_blog/2016/04/conspicuous-mediocrity.html'>Conspicuous mediocrity</a>
			</h3><!-- .entry-header -->
			
			<div class='entry-content'>
			
			<div class='entry-body'>
				<p><a href='http://sethgodin.typepad.com/seths_blog/2013/10/understanding-luxury-goods.html' target='_blank'>Luxury goods</a> originated as a way for the wealthy to both show off their resources and possess a scarce, coveted item of better functionality.</p>
<p>Over time, as luxury goods have become more competitive (it&#39;s a profitable niche if you can find it) a variation is becoming more common: goods and services that aren&#39;t better (in fact, in some cases, not even that good). At some level, they&#39;re proud of this inferiority.</p>
<p>The thinking is, &quot;If you have to ask if it&#39;s any good, you can&#39;t afford it.&quot;</p>
<p>And so we have cars, hotels and restaurants that are far more expensive <em>and</em> dramatically inferior to what a smart shopper could have chosen instead. What&#39;s for sale isn&#39;t performance or reliability. Merely exclusivity.</p>
<p>They offer the customer the satisfaction of looking around the room and saying, &quot;yep, I&#39;m here.&quot;</p>
<p>But it&#39;s a risky strategy, because sooner or later the frequent breakdowns, the lousy service or the poor design communicate to the well-heeled customer, &quot;this merely makes me look stupid.&quot;</p>
<p>No one likes looking stupid.</p>
			</div><!-- .entry-body -->
			
			
			
			</div><!-- .entry-content -->
			
			<div class='entry-footer'>
				<p class='entry-footer-info'>
			
				Posted by <a href='http://profile.typepad.com/sethgodin'>Seth Godin</a> on April 12, 2016
				
							
							
				<span class='separator'>|</span>
				
				<span class='entry-footer-permalink'><a href='http://sethgodin.typepad.com/seths_blog/2016/04/conspicuous-mediocrity.html'>Permalink</a></span>
				
				<!-- comment count -->
				
			
				
			
				</p><!-- .entry-footer-info -->

				
				
				<!-- post footer links -->
				<script src='http://platform.linkedin.com/in.js' type='text/javascript'></script>
<div class='share-counts'>
<p>
<span class='entry-footer-links-twitter'><iframe allowtransparency='true' frameborder='0' scrolling='no' src='http://platform.twitter.com/widgets/tweet_button.html?url=http://sethgodin.typepad.com/seths_blog/2016/04/conspicuous-mediocrity.html&text=Conspicuous mediocrity&count=1' style='width:110px; height:20px;'></iframe></span> <span class='entry-footer-separator'>|</span> <span class='entry-footer-links-plusone'><g:plusone size='medium' href='http://sethgodin.typepad.com/seths_blog/2016/04/conspicuous-mediocrity.html' count='true'></g:plusone></span> <span class='entry-footer-separator'>|</span> <script type='IN/Share' data-counter='right' data-url='http://sethgodin.typepad.com/seths_blog/2016/04/conspicuous-mediocrity.html'></script> <span class='entry-footer-separator'>|</span> <span class='entry-footer-links-facebook_like'><iframe src='http://www.facebook.com/plugins/like.php?href=http://sethgodin.typepad.com/seths_blog/2016/04/conspicuous-mediocrity.html&amp;send=false&amp;layout=button_count&amp;show_faces=false&amp;action=like&amp;colorscheme=light&amp;font=arial&amp;height=21' scrolling='no' frameborder='0' style='border:none; overflow:visible; height:21px;' allowTransparency='true'></iframe></span>
</p></div>
				
			</div><!-- .entry-footer -->
			
			</div><!-- .entry -->
			
			
			
				<div class='pager-bottom pager-entries pager content-nav'>
				<div class='pager-inner'>
					
			
					
			
					<span class='pager-right'>
						<a href='http://sethgodin.typepad.com/seths_blog/page/2/'><span class='pager-label'>Older Posts</span>
							<span class='chevron'>&#187;</span></a>
					</span>
				</div><!-- .pager-inner -->
				</div><!-- .pager-bottom .pager-entries .pager .content-nav -->
			
			
			
			
			
			</div><!-- #beta-inner -->
		</div><!-- #beta -->
		
		</div><!-- #pagebody-inner -->
	</div><!-- #pagebody -->

	<!-- book footer -->
	<!--- $MTInclude module='sb-books-footer'$ --->
	
	</div><!-- #container-inner -->
</div><!-- #container -->

<!-- +1 render call -->
<script type='text/javascript'> 
  (function() {
    var po = document.createElement('script'); po.type = 'text/javascript'; po.async = true;
    po.src = 'https://apis.google.com/js/plusone.js';
    var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(po, s);
  })();
</script>


<script type='text/javascript'>
<!--
var extra_happy = Math.floor(1000000000 * Math.random());
document.write('<img src='http://www.typepad.com/t/stats?blog_id=3511&amp;user_id=2123&amp;page=' + escape(location.href) + '&amp;referrer=' + escape(document.referrer) + '&amp;i=' + extra_happy + '' width='1' height='1' alt='' style='position: absolute; top: 0; left: 0;' />');
// -->
</script>



<!-- Start Quantcast tag -->
<script type='text/javascript' src='http://edge.quantserve.com/quant.js'></script>
<script type='text/javascript'>_qoptions = { tags:'typepad.core' }; _qacct='p-fcYWUmj5YbYKM'; quantserve();</script>
<noscript>
<a href='http://www.quantcast.com/p-fcYWUmj5YbYKM' target='_blank'><img src='http://pixel.quantserve.com/pixel/p-fcYWUmj5YbYKM.gif?tags=typepad.core' style='display: none' border='0' height='1' width='1' alt='Quantcast'/></a>
</noscript>
<!-- End Quantcast tag -->
<!-- Begin comScore Tag -->
<script>
document.write(unescape('%3Cscript src='' + (document.location.protocol == 'https:' ? 'https://sb' : 'http://b') + '.scorecardresearch.com/beacon.js'%3E%3C/script%3E'));
</script>
<script>
COMSCORE.beacon({
  c1: 2,
  c2: '6035669',
  c3: '',
  c4: 'http://sethgodin.typepad.com/seths_blog/',
  c5: '',
  c6: '',
  c15: ''
});
</script>
<noscript>
  <img src='http://b.scorecardresearch.com/b?c1=2&c2=6035669&c3=&c4=http%3A%2F%2Fsethgodin.typepad.com%2Fseths_blog%2F&c5=&c6=&c15=&cv=1.3&cj=1' style='display:none' width='0' height='0' alt='' />
</noscript>
<!-- End comScore Tag -->
<!-- Begin disqus Tag -->

<!-- End disqus Tag -->
</body>
</html>
<!-- ph=1 -->
";
            return htmlCode;
        }
    }
}
