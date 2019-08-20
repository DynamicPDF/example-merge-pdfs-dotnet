using ceTe.DynamicPDF.Merger;
using System;
using System.Text.RegularExpressions;

namespace example_merge_pdfs_dotnet
{
    // This examples shows how to merge PDFs together with and without options.
    // It references the ceTe.DynamicPDF.CoreSuite.NET NuGet package.
    class Program
    {
        static void Main(string[] args)
        {
            Merge2Pdfs();

            MergeAndAppendPdfs();

            MergeWithOptions();
        }

        // A simple Merge of two PDF files.
        // Use the ceTe.DynamicPDF.Merger namespace for the MergeDocument class.
        static void Merge2Pdfs()
        {
            //Create MergeDocument object with source PDFs to Merge
            MergeDocument document = MergeDocument.Merge(GetResourcePath("doc-a.pdf"), GetResourcePath("doc-b.pdf"));

            //Save the merged PDF
            document.Draw("output-simple-merge.pdf");
        }

        // A simple merge and then appending another PDF.
        // Use the ceTe.DynamicPDF.Merger namespace for the MergeDocument class.
        static void MergeAndAppendPdfs()
        {
            //Create MergeDocument object with source PDFs to Merge
            MergeDocument document = MergeDocument.Merge(GetResourcePath("doc-a.pdf"), GetResourcePath("doc-b.pdf"));

            //Append a PDF document
            document.Append(GetResourcePath("doc-c.pdf"), 1, 2);

            //Save the merged PDF
            document.Draw("output-with-append.pdf");
        }

        // A simple merge and appending two other PDFs with options.
        // Use the ceTe.DynamicPDF.Merger namespace for the MergeDocument and MergeOptions classes.
        static void MergeWithOptions()
        {
            //Create MergeDocument with MergeOptions
            var document = new MergeDocument(GetResourcePath("doc-a.pdf"), MergeOptions.All);

            var optionsNoOutlines = MergeOptions.Append;
            optionsNoOutlines.Outlines = false;
            document.Append(GetResourcePath("doc-with-outline.pdf"), optionsNoOutlines);

            var optionsNoAnnotations = MergeOptions.Append;
            optionsNoAnnotations.PageAnnotations = false;
            document.Append(GetResourcePath("doc-with-note.pdf"), optionsNoAnnotations);

            document.Draw("output-with-options.pdf");
        }

        // This is a helper function to get the full path to a file in the Resources folder.
        public static string GetResourcePath(string inputFileName)
        {
            var exePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
            Regex appPathMatcher = new Regex(@"(?<!fil)[A-Za-z]:\\+[\S\s]*?(?=\\+bin)");
            var appRoot = appPathMatcher.Match(exePath).Value;
            return System.IO.Path.Combine(appRoot, "Resources", inputFileName);
        }
    }
}