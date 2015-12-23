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
            string a = "01gdsfg hjuhgds&1hvjdshv$$xgcdhsghjgkj&4dhvhsdgvjksE01gdsfg hjuhgds&1hvjdshv$$xgcdhsghjgkj&4dhvhsdgvjksE";
            getNotebook(a);
        }

        private void getNotebook(string a)
        {
            char[] charArray = a.ToCharArray();
            string sub;
            string s_temp, s_full;
            string S_xinzhi;
            int i = 0, j = 2, k = 0, cnt = 0, m;
            TextBox h=new TextBox();
            h.Text = "add";
            while (charArray[i] != '\0')
            {
                s_full = "\0";
                for (i = j; charArray[i] != '&'; i++) { }
                sub = a.Substring(j, i - j);
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
                    s_full = S_xinzhi;
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
                }
                h.Text = s_full;
                this.showNotebook.Children.Add(h);
            }
        }
    }
}
