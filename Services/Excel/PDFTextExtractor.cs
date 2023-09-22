using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using iText.Layout;
using iText.Layout.Element;

namespace Services
{
    public static class PDFTextExtractor
    {
        public static List<string> ExtractText(Stream pdfFileStream)
        {
            //创建一个PdfReader对象，用来读取pdf文件
            PdfReader pdfReader = new PdfReader(pdfFileStream);
            //创建一个PdfDocument对象，用于操作pdf文档
            PdfDocument pdfDocument = new PdfDocument(pdfReader);
            //创建一个StringBuilder对象，来存储提取的文本
            List<string> textStrings = new List<string>();
            //获取pdf文档的总页数
            int pageCount = pdfDocument.GetNumberOfPages();
            //遍历每一页
            for (int i = 1; i <= pageCount; i++)
            {
                //获取当前页的PdfPage对象
                PdfPage pdfPage = pdfDocument.GetPage(i);
                //创建一个ITextExtractionStrategy对象，用于指定提取文本的策略
                //ITextExtractionStrategy strategy = new LocationTextExtractionStrategy();
                ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();
                //使用PdfTextExtractor类的GetTextFromPage方法，根据指定的策略提取当前页的文本
                string pageText = PdfTextExtractor.GetTextFromPage(pdfPage, strategy);
                //将提取的文本追加到StringBuilder对象中
                textStrings.Add(pageText.Replace(" ",""));
            }
            //关闭PdfDocument对象
            pdfDocument.Close();
            //返回StringBuilder对象中的字符串
            return textStrings;
        }
    }
}
