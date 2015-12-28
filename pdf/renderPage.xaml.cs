using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Storage.Pickers;
using Windows.Storage;
using foxitSDK;
using Windows.UI.Xaml.Media.Imaging;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

/*
PDF SDK DEFINE:

 @brief	No rotation. 
#define	FSCRT_PAGEROTATION_0		0
 @brief	Rotate 90 degrees in clockwise direction.
#define	FSCRT_PAGEROTATION_90		1
 @brief	Rotate 180 degrees in clockwise direction. 
#define	FSCRT_PAGEROTATION_180		2
 @brief	Rotate 270 degrees in clockwise direction. 
#define	FSCRT_PAGEROTATION_270		3

#define FSCRT_ERRCODE_SUCCESS					0
#define FSCRT_ERRCODE_ERROR						-1
typedef int						FS_RESULT;
*/


namespace pdf
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class renderPage : Page
    {
        public renderPage()
        {
            this.InitializeComponent();

            /////////////////////////////////

            m_bReleaseLibrary = false;
            m_SDKDocument = null;

            m_PDFFunction = new Inherited_PDFFunction();

            //Initialize, otherwise no method of SDK can be used.
            int iRet = m_PDFFunction.FSDK_Initialize();
            if (0 != iRet)
            {
                //ShowErrorLog("Error: Fail to initialize SDK library.", ref new UICommandInvokedHandler(this, &demo_view::renderPage::ReturnCommandInvokedHandler), true, iRet);
                return;
            }
            m_bReleaseLibrary = true;

            m_SDKDocument = new FSDK_Document();

            m_PDFDoc.pointer = 0;////////////////////
            m_PDFPage.pointer = 0;

            m_iCurPageIndex = 0;
            m_fPageWidth = 0.0f;
            m_fPageHeight = 0.0f;

            m_iStartX = 0;
            m_iStartY = 0;
            m_iRenderAreaSizeX = 0;
            m_iRenderAreaSizeY = 0;
            m_iRotation = 0;

            m_dbScaleDelta = 0.05f;
            m_dbScaleFator = 1.0f;
            m_dbCommonFitWidthScale = 1.0f;
            m_dbCommonFitHeightScale = 1.0f;
            m_dbRotateFitWidthScale = 1.0f;
            m_dbRotateFitHeightScale = 1.0f;

            m_bFitWidth = false;
            m_bFitHeight = false;
            m_mousestate = false;
        }

        void ReleaseResources()
        {
            if (m_SDKDocument != null)
                m_SDKDocument.ReleaseResource();
            m_PDFFunction.FSDK_Finalize();
            m_bReleaseLibrary = false;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            m_pPDFFile = e.Parameter as Windows.Storage.StorageFile;
            if(m_pPDFFile != null)
            {
                m_PDFDoc.pointer = 0;////////////////////
                m_PDFPage.pointer = 0;

                m_iCurPageIndex = 0;
                m_fPageWidth = 0.0f;
                m_fPageHeight = 0.0f;

                m_iStartX = 0;
                m_iStartY = 0;
                m_iRenderAreaSizeX = 0;
                m_iRenderAreaSizeY = 0;
                m_iRotation = 0;

                m_dbScaleDelta = 0.05f;
                m_dbScaleFator = 1.0f;
                m_dbCommonFitWidthScale = 1.0f;
                m_dbCommonFitHeightScale = 1.0f;
                m_dbRotateFitWidthScale = 1.0f;
                m_dbRotateFitHeightScale = 1.0f;

                m_bFitWidth = false;
                m_bFitHeight = false;
                m_mousestate = false;
                OpenPDFDocument(m_pPDFFile);
            }
        }

        private async void OpenPDFDocument(Windows.Storage.StorageFile file)
        {
            if (file != null)
            {
                Windows.Storage.FileProperties.BasicProperties properties = await file.GetBasicPropertiesAsync();
                m_SDKDocument = new FSDK_Document();
                //Load PDF document
                int result = await m_SDKDocument.OpenDocumentAsync(file, (int)properties.Size);
                if(result != 0)
                {
                    //showerrorlog
                    return;
                }
                m_PDFDoc.pointer = m_SDKDocument.m_hDoc.pointer;
                //Load PDF page
                result = LoadPage(m_iCurPageIndex);
                if(result != 0)
                {
                    //showerrorlog
                    return;
                }
                //Show PDF page to device.
                m_dbScaleFator = (m_dbCommonFitWidthScale < m_dbCommonFitHeightScale) ? m_dbCommonFitWidthScale : m_dbCommonFitHeightScale;
                ShowPage();
                
            }
            else
            {
                //ShowErrorLog("Error: Fail to pick a valid PDF file.", ref new UICommandInvokedHandler(this, renderPage.ReturnCommandInvokedHandler));
                return;
            }
        }

        private int LoadPage(int iPageIndex)
        {// To load and parse page.
            int result = -1;
            if (m_PDFDoc.pointer == 0)
            {
                //ShowErrorLog("Error: No PDF document has been loaded successfully.", ref new UICommandInvokedHandler(this, &demo_view::renderPage::ReturnCommandInvokedHandler));
                return result;
            }
            if (m_PDFPage.pointer != 0)
            {
                m_PDFFunction.My_Page_Clear(m_PDFPage);
                m_PDFPage.pointer = 0;
            }
            result = m_SDKDocument.LoadPageSync(iPageIndex);
            if (0 == result)
            {
                m_PDFPage.pointer = m_SDKDocument.m_hPage.pointer;
                m_iCurPageIndex = iPageIndex;
                GetPageInfo();
            }
            else
            {
                //ShowErrorLog("Error: Fail to load and parse a page.", ref new UICommandInvokedHandler(this, &demo_view::renderPage::ReturnCommandInvokedHandler),true, ret);
                ;
            }
            return result;
        }

        public void GetPageInfo()
        {// To get page size and prepare some necessary data.
            if (m_PDFPage.pointer == 0)
            {
                //ShowErrorLog("Error: No PDF page has been loaded successfully.", ref new UICommandInvokedHandler(this, &demo_view::renderPage::ReturnCommandInvokedHandler));
                return;
            }
            int result = m_PDFFunction.My_Page_GetSize(m_PDFPage, out m_fPageWidth,out m_fPageHeight);///////////////////
            if (result == 0)
            {
                double scrollViewerW = scroll_pdf.ActualWidth;
                double scrollViewerH = scroll_pdf.ActualHeight;

                m_dbCommonFitWidthScale = scrollViewerW / m_fPageWidth;
                m_dbCommonFitHeightScale = scrollViewerH / m_fPageHeight;

                m_dbRotateFitWidthScale = scrollViewerW / m_fPageHeight;
                m_dbRotateFitHeightScale = scrollViewerH / m_fPageWidth;
            }
        }

        public async void ShowPage()
        {//To render PDF page, finally to the image control.	
         //Calculate render size.
            CalcRenderSize();
            PixelSource bitmap =  new PixelSource();
            bitmap.Width = (int)m_iRenderAreaSizeX;
            bitmap.Height = (int)m_iRenderAreaSizeY;
            Windows.Storage.Streams.IRandomAccessStreamWithContentType stream = await m_SDKDocument.RenderPageAsync(bitmap, m_iStartX, m_iStartY, m_iRenderAreaSizeX, m_iRenderAreaSizeY, m_iRotation);
            if (stream == null)
            {
                //ShowErrorLog("Error: Fail to render page.", ref new UICommandInvokedHandler(this, &demo_view::renderPage::ReturnCommandInvokedHandler),true, FSCRT_ERRCODE_ERROR);
                return;
            }
            Windows.UI.Xaml.Media.Imaging.BitmapImage bmpImage = new Windows.UI.Xaml.Media.Imaging.BitmapImage();
            bmpImage.SetSource(stream);
            image.Width = (int)m_iRenderAreaSizeX;
            image.Height = (int)m_iRenderAreaSizeY;
            image.Source = bmpImage;
            
        }
        
        public void CalcRenderSize()
        {// To calculate render size.
            if (1 == m_iRotation || 3 == m_iRotation)
            {
                if (m_bFitWidth)
                    m_dbScaleFator = m_dbRotateFitWidthScale;
                if (m_bFitHeight)
                    m_dbScaleFator = m_dbRotateFitHeightScale;

                m_iRenderAreaSizeX = (int)(m_fPageHeight * m_dbScaleFator);
                m_iRenderAreaSizeY = (int)(m_fPageWidth * m_dbScaleFator);
            }
            else
            {
                if (m_bFitWidth)
                    m_dbScaleFator = m_dbCommonFitWidthScale;
                if (m_bFitHeight)
                    m_dbScaleFator = m_dbCommonFitHeightScale;

                m_iRenderAreaSizeX = (int)(m_fPageWidth * m_dbScaleFator);
                m_iRenderAreaSizeY = (int)(m_fPageHeight * m_dbScaleFator);
            }
        }

        public void Search()
        {
            if(m_PDFDoc.pointer == 0)
            {
                //showerrorlog
                return;
            }
            int result = -1;
            int iPageCount = m_PDFFunction.My_Doc_CountPages(m_PDFDoc);
            for(int i = 0;i < iPageCount;i++)
            {
                result = LoadPage(i);
                if(result != 0)
                {
                    return;
                }
                
            }
        }
        private Windows.Storage.StorageFile	        m_pPDFFile;
		private bool                                m_bReleaseLibrary;             // A flag used to indicate if SDK library has been initialized successfully and needs to be released.
        private FSDK_Document                       m_SDKDocument;
        private DocHandle                           m_PDFDoc;
        private PageHandle                                  m_PDFPage;
		//FSCRT_DOCUMENT m_PDFDoc;           // SDK document handle of current loaded document.
        //FSCRT_PAGE m_PDFPage;              // SDK page handle of current loaded page.
        
        private int                                 m_iCurPageIndex;               //Index of current loaded page.
        private float                               m_fPageWidth;             // Page width of current page.
        private float                               m_fPageHeight;            // Page Height of current page.

        //used for rendering page;
        private int m_iStartX;
        private int m_iStartY;
        private int m_iRenderAreaSizeX;
        private int m_iRenderAreaSizeY;
        private int m_iRotation;

        //Used for zooming.
        private double m_dbScaleDelta;
        private double m_dbScaleFator;
        private double m_dbCommonFitWidthScale;
        private double m_dbCommonFitHeightScale;
        private double m_dbRotateFitWidthScale;
        private double m_dbRotateFitHeightScale;
        private bool m_bFitWidth;
        private bool m_bFitHeight;

        private Point m_BeginLocation;
        private Point m_EndLocation;
        private Inherited_PDFFunction m_PDFFunction;
        private bool m_mousestate;

        private void Click_BTN_NextPage(object sender, RoutedEventArgs e)
        {//Button click event:to turn to the next page
            if (m_PDFDoc.pointer == 0 || m_PDFPage.pointer == 0)
            {
                //showerrorlog
                return;
            }
            int iPageCount;
            iPageCount = m_PDFFunction.My_Doc_CountPages(m_PDFDoc);
            int iPageIndex = m_iCurPageIndex;
            if(iPageIndex < iPageCount - 1)
            {
                iPageIndex++;
                int result = LoadPage(iPageIndex);
                if(result != 0)
                {
                    return;
                }
                ShowPage();
            }
        }

        private void Click_BTN_PrePage(object sender, RoutedEventArgs e)
        {// Button click event: to turn to the previous page
            if (m_PDFDoc.pointer == 0 || m_PDFPage.pointer == 0)
            {
                //ShowErrorLog("Error: No PDF document or page has been loaded successfully.", ref new UICommandInvokedHandler(this, &demo_view::renderPage::ReturnCommandInvokedHandler));
                return;
            }
            int iPageCount;
            iPageCount = m_PDFFunction.My_Doc_CountPages(m_PDFDoc);
            int iPageIndex = m_iCurPageIndex;

            if (iPageIndex > 0)
            {
                iPageIndex--;
                int result = LoadPage(iPageIndex);
                if (result != 0)
                {
                    return;
                }
                ShowPage();
            }
        }

        private void Click_BTN_ZoomIn(object sender, RoutedEventArgs e)
        {// Button click event: to zoom in the page.
            if (m_PDFDoc.pointer == 0 || m_PDFPage.pointer == 0)
            {
                //ShowErrorLog("Error: No PDF document or page has been loaded successfully.", ref new UICommandInvokedHandler(this, &demo_view::renderPage::ReturnCommandInvokedHandler));
                return;
            }

            if (m_dbScaleFator + m_dbScaleDelta > 2.5)
                return;

            m_bFitHeight = false;
            m_bFitWidth = false;
            this.m_dbScaleFator += this.m_dbScaleDelta;
            ShowPage();

        }

        private void Click_BTN_ZoomOut(object sender, RoutedEventArgs e)
        {// Button click event: to zoom out the page.
            if (m_PDFDoc.pointer == 0 || m_PDFPage.pointer == 0)
            {
                //ShowErrorLog("Error: No PDF document or page has been loaded successfully.", ref new UICommandInvokedHandler(this, &demo_view::renderPage::ReturnCommandInvokedHandler));
                return;
            }

            if (m_dbScaleFator - m_dbScaleDelta < 0.5)
                return;

            m_bFitHeight = false;
            m_bFitWidth = false;
            this.m_dbScaleFator -= this.m_dbScaleDelta;
            ShowPage();

        }

        private void Click_BTN_FitHeight(object sender, RoutedEventArgs e)
        {
           
        }

        private void Click_BTN_FitWidth(object sender, RoutedEventArgs e)
        {

        }

        private void Click_BTN_ActualSize(object sender, RoutedEventArgs e)
        {

        }

        private void Click_BTN_RotateRight(object sender, RoutedEventArgs e)
        {

        }

        private void Click_BTN_RotateLeft(object sender, RoutedEventArgs e)
        {

        }

        private void GetBeginLocation(object sender, PointerRoutedEventArgs e)
        {
            if(m_mousestate == false)
            {
                Windows.UI.Input.PointerPoint location = e.GetCurrentPoint(image);
                m_BeginLocation = location.Position;
                m_mousestate = true;
            }
        }

        private async void GetEndLocation(object sender, PointerRoutedEventArgs e)
        {
            if(m_mousestate == true)
            {
                Windows.UI.Input.PointerPoint location = e.GetCurrentPoint(image);
                m_EndLocation = location.Position;
                m_mousestate = false;
                if (m_BeginLocation.X == m_EndLocation.X && m_BeginLocation.Y == m_EndLocation.Y)
                {
                    //get word
                    System.String word = null ;
                    word = m_PDFFunction.GetWordFromLocation(m_PDFPage,(float)m_BeginLocation.X, (float)m_BeginLocation.Y,m_iStartX, m_iStartY, m_iRenderAreaSizeX, m_iRenderAreaSizeY, m_iRotation);
                }
                else
                {
                    //highlight
                }
            }
            
        }

        private void GetMoveLocation(object sender, PointerRoutedEventArgs e)
        {
            if(m_mousestate == true )
            {
                Windows.UI.Input.PointerPoint location = e.GetCurrentPoint(image);
                m_EndLocation = location.Position;
                //runtime highlight
            }
        }

        public void Searchword_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void Previous_Click(object sender, RoutedEventArgs e)
        {
           
            
           
        }
       
                private void getNotebook(string a)
                {
                    GridLength hleng = new GridLength(250);
                    currtbook.Width = hleng;
                    char[] charArray = a.ToCharArray();
                    string sub;
                    string s_temp, s_full;
                    string S_xinzhi;
                    int i = 0, j = 0, k = 0, cnt = 0;
                    int m = 0;
                    ImageBrush brush = new ImageBrush();
                    ImageBrush brush1 = new ImageBrush();
                    brush.ImageSource = new BitmapImage(
                                new Uri("ms-appx:///Assets/0006.jpg", UriKind.RelativeOrAbsolute)
                           );
                    while (j != a.Length)
                    {
                        s_full = "\0";
                        for (i = j; charArray[i] != '&'; i++) { }
                        sub = a.Substring(j + 2, i - j - 2);
                        s_full = sub + '\n';

                        while (charArray[j] != 'E')
                        {
                            j = i + 2;
                            k = k + 1;
                            cnt = 0;
                            switch (charArray[i + 1])
                            {
                                case '1':
                                    S_xinzhi = "prop. ";
                                    break;
                                case '2':
                                    S_xinzhi = "int. ";
                                    break;
                                case '3':
                                    S_xinzhi = "abbr. ";
                                    break;
                                case '4':
                                    S_xinzhi = "n. ";
                                    break;
                                case '5':
                                    S_xinzhi = "v. ";
                                    break;
                                case '6':
                                    S_xinzhi = "adj. ";
                                    break;
                                case '7':
                                    S_xinzhi = "pron. ";
                                    break;
                                case '8':
                                    S_xinzhi = "art. ";
                                    break;
                                case '9':
                                    S_xinzhi = "na. ";
                                    break;
                                default:
                                    S_xinzhi = "more. ";
                                    break;
                            }
                            s_temp = S_xinzhi;
                            s_full = s_full + S_xinzhi;
                            for (i = j; charArray[i] != '&'; i++)
                            {
                                if (charArray[i] == '$' && charArray[i + 1] == '$')
                                {
                                    sub = a.Substring(j, i - j);
                                    i = i + 1;
                                    j = i + 1;
                                    cnt = cnt + 1;
                                    if (cnt <= 4)
                                    {
                                        s_temp = s_temp + sub + ';';
                                    }
                                    s_full = s_full + sub + ';';
                                    if (cnt == 38)
                                        break;
                                }
                                if (charArray[i] == 'E')
                                    break;
                            }
                            if (cnt == 38)
                            {

                                while (charArray[j] < '0' || charArray[j] > '9')
                                {
                                    if (charArray[j] == 'E')
                                        break;
                                    j++;
                                }
                                i = j;
                                if (charArray[j] == 'E')
                                    break;
                            }
                            else
                            {
                                sub = a.Substring(j, i - j);

                                cnt = cnt + 1;
                                if (cnt <= 4)
                                    s_temp = s_temp + sub + ';';
                                s_full = s_full + sub + ';';

                                if (charArray[i] == 'E')
                                    break;
                            }
                            s_full = s_full + '\n';

                        }
                        j = i + 1;
                        Grid g = new Grid();
                        Button close = new Button();
                        TextBlock h = new TextBlock();

                        g.Margin = new Thickness(12, 12, 12, 12);
                        g.Background = brush;
                        h.Width = 200;
                        h.Height = 150;
                        h.TextWrapping = TextWrapping.Wrap;
                        h.Text = s_full;
                        g.Children.Add(h);
                        close.Content = "------";
                        close.Name = "name" + m;
                        g.Name = "name" + m;
                        close.HorizontalAlignment = HorizontalAlignment.Right;
                        close.VerticalAlignment = VerticalAlignment.Top;
                        close.Height = 25;
                        close.Click += Closeunit_Click;
                        close.Background = brush1;
                        g.Children.Add(close);
                        currentNotebook.Children.Add(g);
                        m = m + 1;
                    }
                }
        /*  
         private void getNotebook(string a)
         {
             pdf.currentNotebook newlist = new pdf.currentNotebook();
             newlist.getword(a);

         }

     */
        private void Closeunit_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            foreach (Grid gri in this.currentNotebook.Children)
            {
                
                    if (gri.Name == btn.Name)
                    {
                        gri.Visibility = Visibility.Collapsed;
                    }
               
            }
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private async void Newpdf_Click(object sender, RoutedEventArgs e)
        {
            Windows.Storage.Pickers.FileOpenPicker openPicker = new Windows.Storage.Pickers.FileOpenPicker();
            openPicker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
            openPicker.FileTypeFilter.Add(".pdf");
            Windows.Storage.StorageFile pdf_file = await openPicker.PickSingleFileAsync();
            if (pdf_file != null)
            {
                this.Frame.Navigate(typeof(renderPage), pdf_file);
            }
        }

        private void closepdf_Click(object sender, RoutedEventArgs e)
        {
            
        }

        public bool flag_notebook=false;
        private void Like_Click(object sender, RoutedEventArgs e)
        {
            if (flag_notebook == false)
           {
                string a = "01word pron&1释义$$一个很长的释义$$一个很长长长长长长长长长长长长长长长长的释义&5释义E01词语 音标&4释义E01word pron&1释义$$一个很长的释义$$一个很长长长长长长长长长长长长长长长长的释义&5释义E01word pron&1释义$$一个很长的释义$$一个很长长长长长长长长长长长长长长长长的释义&5释义E01word pron&1释义$$一个很长的释义$$一个很长长长长长长长长长长长长长长长长的释义&5释义E01word pron&1释义$$一个很长的释义$$一个很长长长长长长长长长长长长长长长长的释义&5释义E01word pron&1释义$$一个很长的释义$$一个很长长长长长长长长长长长长长长长长的释义&5释义E01word pron&1释义$$一个很长的释义$$一个很长长长长长长长长长长长长长长长长的释义&5释义E01word pron&1释义$$一个很长的释义$$一个很长长长长长长长长长长长长长长长长的释义&5释义E01word pron&1释义$$一个很长的释义$$一个很长长长长长长长长长长长长长长长长的释义&5释义E01word pron&1释义$$一个很长的释义$$一个很长长长长长长长长长长长长长长长长的释义&5释义E";
                getNotebook(a);
                flag_notebook = true;
            }
            else
           {
               currentNotebook.Children.Clear();
               flag_notebook = false;
                GridLength hleng = new GridLength(0);
                currtbook.Width = hleng;
            }
        }
    }
}
