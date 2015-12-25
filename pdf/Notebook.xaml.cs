using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace pdf
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Notebook : Page
    {
        public Notebook()
        {
            this.InitializeComponent();
            string a = "01word pron&1释义$$一个很长的释义$$一个很长长长长长长长长长长长长长长长长的释义&5释义E01词语 音标&4释义E01word pron&1释义$$一个很长的释义$$一个很长长长长长长长长长长长长长长长长的释义&5释义E01word pron&1释义$$一个很长的释义$$一个很长长长长长长长长长长长长长长长长的释义&5释义E01word pron&1释义$$一个很长的释义$$一个很长长长长长长长长长长长长长长长长的释义&5释义E01word pron&1释义$$一个很长的释义$$一个很长长长长长长长长长长长长长长长长的释义&5释义E01word pron&1释义$$一个很长的释义$$一个很长长长长长长长长长长长长长长长长的释义&5释义E01word pron&1释义$$一个很长的释义$$一个很长长长长长长长长长长长长长长长长的释义&5释义E01word pron&1释义$$一个很长的释义$$一个很长长长长长长长长长长长长长长长长的释义&5释义E01word pron&1释义$$一个很长的释义$$一个很长长长长长长长长长长长长长长长长的释义&5释义E01word pron&1释义$$一个很长的释义$$一个很长长长长长长长长长长长长长长长长的释义&5释义E";
            getNotebook(a);
        }

        private void getNotebook(string a)
        {
            char[] charArray = a.ToCharArray();
            string sub;
            string s_temp, s_full;
            string S_xinzhi;
            int i = 0, j = 0, k = 0, cnt = 0;
            int m = 0,mm=0;
            ImageBrush brush = new ImageBrush();
            ImageBrush brush1 = new ImageBrush();
            brush.ImageSource = new BitmapImage(
                        new Uri("ms-appx:///Assets/0006.jpg", UriKind.RelativeOrAbsolute)
                   );
            while (j != a.Length)
            {
                s_full = "\0";
                for (i = j; charArray[i] != '&'; i++) { }
                sub = a.Substring(j+2, i - j-2);
                s_full = sub + '\n';
                StackPanel stackpanel;
                while (charArray[j] != 'E')
                {
                    if (m % 6 == 0)
                    {
                        mm = mm + 1;
                        stackpanel = new StackPanel();
                        stackpanel.Orientation = Orientation.Horizontal;
                        
                        stackpanel.Name = "Number"+mm;
                        showNotebook.Children.Add(stackpanel);
                        
                    }
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
                    s_full = s_full+S_xinzhi;
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
                //        close.Tag = m;
                //        g.Tag = m;
                close.Name = "name"+m;
                g.Name = "name"+m;
                close.HorizontalAlignment = HorizontalAlignment.Right;
                close.VerticalAlignment = VerticalAlignment.Top;
                close.Height = 25;
                close.Click += CloseNotebook_Click;
                close.Background = brush1;
                g.Children.Add(close);
                foreach (StackPanel ctl in this.showNotebook.Children)
                {
                   
                        if (ctl.Name == "Number"+mm)
                        {
                        ctl.Children.Add(g);
                        m = m + 1;
                    }
                   
                }
               
                //  this.showNotebook.Children.Add(h);
            }
           
        }

        private void CloseNotebook_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            
            foreach (StackPanel ctl in this.showNotebook.Children)
            {
                foreach (Grid gri in ctl.Children)
                {


                    if (gri.Name == btn.Name)
                    {
                        gri.Visibility = Visibility.Collapsed;
                    }
        //            if (gri.Tag == btn.Tag)
        //            {
        //                ctl.Visibility = Visibility.Collapsed;
       //             }
                }
            }
            //    btn.Visibility = Visibility.Collapsed;
            //    throw new NotImplementedException();
        }
    }
}
