using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace vfilename
{
    /// <summary>
    /// ListWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ListWindow : Window
    {
        public ListWindow()
        {
            InitializeComponent();
            hiddenGrid.ColumnDefinitions[0].Width = new GridLength(SystemParameters.VerticalScrollBarWidth*2);
            /*
            lvItems = new List<ListItem>();
            lvItems.Add(new ListItem() { ProjectName = "测试一", UserName = "刘志勇", DateTime = "2020", Descrption = "又是测试" });
            lvItems.Add(new ListItem() { ProjectName = "测试一", UserName = "刘志勇", DateTime = "2021", Descrption = "还是测试" });
            lvItems.Add(new ListItem() { ProjectName = "测试项目", UserName = "李明", DateTime = "2022", Descrption = "总是测试"});
            
            lv.ItemsSource = lvItems;
            */
        }
        public List<ListItem> lvItems;

        public List<ListItem> allItems;

        public static IEnumerable<string> GetFiles(string path,
                       string filter = "*",
                       string searchPatternExpression = "",
                       SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            Regex reSearchPattern = new Regex(searchPatternExpression, RegexOptions.IgnoreCase);
            return Directory.EnumerateFiles(path, filter, searchOption)
                            .Where(file => reSearchPattern.IsMatch(file));
        }

        public void InitList(string Folder)
        {
            allItems = new List<ListItem>();
            IEnumerable<string> files = GetFiles(Folder, "*.*", RegexStrClass.RegexStr, SearchOption.AllDirectories)
                .OrderByDescending(x => x, new SpecialComparer());
            
            foreach(string f in files)
            {
                Match m = Regex.Match(f, RegexStrClass.RegexStr, RegexOptions.IgnoreCase);
                //string tmp = "InitList / RegexMatch\r\n";
                //for (int i = 0; i < m.Groups.Count; i++)
                //{
                //    tmp += i.ToString() + " = " + m.Groups[i].Value + "\r\n";
                //}
                //MessageBox.Show(tmp);
                allItems.Add(new ListItem() { ProjectName = m.Groups[1].Value, UserName = m.Groups[2].Value, DateTime = m.Groups[3].Value, Descrption = m.Groups[4].Value, FilePath = f });
            }
            lvItems = allItems;
            lv.ItemsSource = lvItems;
        }

        private void FilterTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string s = ((TextBox)sender).Text;
            if (s == "过滤的关键词")
            {
                return;
            }
            if (s=="")
            {
                lvItems = allItems;
                lv.ItemsSource = lvItems;
                return;
            }

            lvItems = new List<ListItem>();
            foreach (ListItem li in allItems)
            {
                if (li.FilePath.ToLower().IndexOf(s.ToLower()) != -1)
                {
                    lvItems.Add(new ListItem() { ProjectName = li.ProjectName, UserName = li.UserName, DateTime = li.DateTime, Descrption = li.Descrption, FilePath = li.FilePath });
                }
            }
            lv.ItemsSource = lvItems;
        }

        private void FilterTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (((TextBox)sender).Text == "过滤的关键词")
            {
                ((TextBox)sender).Text = "";
            }
            lv.UnselectAll();
        }

        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Topmost = false;
            //MessageBox.Show(lvItems[lv.SelectedIndex].FilePath);
            string ClickedFile = lvItems[lv.SelectedIndex].FilePath;
            string ClickedFileExtension = System.IO.Path.GetExtension(ClickedFile).Replace(".", "").ToUpper();
            string ClickedFileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(ClickedFile);
            string ClickedFileFolder = System.IO.Path.GetDirectoryName(ClickedFile);
            string ExtractFolder = System.IO.Path.Combine(
                ClickedFileFolder, 
                "VFILENAME-"+ClickedFileExtension+"-"+ClickedFileNameWithoutExtension + "-" + DateTime.Now.ToString("yyyy.MM.dd@HH.mm.ss")
                );

            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            if (ClickedFileExtension == "RAR")
            {
                string RarExe = ConfigTxt.Read("rarexe", "");
                if (RarExe == "")
                {
                    MessageBox.Show("WinRAR程序位置没有定义。");
                    return;
                }
                startInfo.FileName = RarExe;
                startInfo.Arguments = "x \"" + ClickedFile + "\" \"" + ExtractFolder + "\\\"";
                //MessageBox.Show(startInfo.Arguments);
            }
            if (ClickedFileExtension == "7Z")
            {
                string SevenZipExe = ConfigTxt.Read("7zexe", "");
                if (SevenZipExe == "")
                {
                    MessageBox.Show("7-Zip程序位置没有定义。");
                    return;
                }
                startInfo.FileName = SevenZipExe;
                startInfo.Arguments = "x \"" + ClickedFile + "\" -o\"" + ExtractFolder + "\\\"";
                //MessageBox.Show(startInfo.Arguments);
            }
            process.StartInfo = startInfo;
            process.Start();
            process.WaitForExit();
            System.Diagnostics.Process.Start(ExtractFolder);
        }
    }

    public class ListItem
    {
        public string ProjectName { get; set; }

        public string UserName { get; set; }

        public string DateTime { get; set; }

        public string Descrption { get; set; }

        public string FilePath { get; set; }
    }

    public class SpecialComparer : IComparer<string>
    {
        public int Compare(string s1, string s2)
        {
            Match m1 = Regex.Match(s1, RegexStrClass.RegexStr, RegexOptions.IgnoreCase);
            Match m2 = Regex.Match(s2, RegexStrClass.RegexStr, RegexOptions.IgnoreCase);
            string t1 = m1.Groups[3].Value;
            string t2 = m2.Groups[3].Value;
            //MessageBox.Show("SpecialCompare "+t1+" ... "+t2);
            return String.Compare(t1, t2);
        }
    }

    public static class RegexStrClass
    {
        public const string RegexStr = @"^.*\\([^\\]*)-([^-\\]*)-(\d\d\d\d\.\d\d\.\d\d@\d\d\.\d\d\.\d\d)-([^\\]*)\.(rar|7z)$";
    }
}
