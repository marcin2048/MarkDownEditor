

using Markdig;
using Markdig.SyntaxHighlighting;
using Microsoft.Web.WebView2.Core;

namespace MarkDownEditor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            webView21.EnsureCoreWebView2Async();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string cnt = richTextBox1.Text;
            var pipeline = new Markdig.MarkdownPipelineBuilder()
                .UseAdvancedExtensions()
                .UseSyntaxHighlighting()
                .Build();
            var res = Markdig.Markdown.ToHtml(cnt, pipeline);

            string css = File.ReadAllText("css.txt");

            string mermaidscript = @"    <script type=""module"">
// select <pre class=""mermaid""> _and_ <pre><code class=""language-mermaid"">
document.querySelectorAll(""div.lang-mermaid, pre>code.lang-mermaid"").forEach($el => {
  // if the second selector got a hit, reference the parent <pre>
  if ($el.tagName === ""CODE"")
    $el = $el.parentElement
  // put the Mermaid contents in the expected <div class=""mermaid"">
  // plus keep the original contents in a nice <details>
  $el.outerHTML = `
    <div class=""mermaid"" >${$el.textContent}</div>
    <details>
      <summary>Diagram source</summary>
      <pre>${$el.textContent}</pre>
    </details>
  `
})
        import mermaid from 'https://cdn.jsdelivr.net/npm/mermaid@10/dist/mermaid.esm.min.mjs';
        mermaid.initialize({ startOnLoad: true });
    </script>";

            res = "<!DOCTYPE html>\n<html>\n<head>\n<style>\n" + css
                + "\n</style>\n</head>\n<body>\n" + res + mermaidscript + "\n</body>\n</html>";


            webView21.CoreWebView2.NavigateToString(res);


        }

        private void button2_Click(object sender, EventArgs e)
        {
            webView21.CoreWebView2.ShowPrintUI();
        }
    }
}