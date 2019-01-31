using System;
using System.Collections.Generic;
namespace MyApp
{
    class Bor {
        public Bor[] a = new Bor[33];
        public bool term;
        public Bor() {
            for (int i = 0; i < 33; ++i) {
                a[i] = null;
                term = false;
            }
        }
    }


    class Program
    {


        static void Ad(ref Bor a, string s, int ind) {
            if (ind == s.Length)
            {
                a.term = true;
                return;
            }
            if (a.a[s[ind] - 'а'] == null)
                a.a[s[ind] - 'а'] = new Bor();
            Ad(ref a.a[s[ind] - 'а'], s, ++ind);

        }


        static Bor Solve(List<string> lst) {
            Bor root = new Bor();
            for (int i = 0; i < lst.Count; ++i) {
                bool f = false;
                for (int j = 0; j < lst[i].Length; ++j)
                    if(lst[i][j] == '-') {
                        f = true;
                        break;
                    }
                if(!f)Ad(ref root, lst[i], 0);

            }
            return root;
        }


        static void print(char[][] a) {
            for (int i = 0; i < a.Length; ++i) {
                for (int j = 0; j < a[i].Length; ++j) {
                    Console.Write(a[i][j] + " ");
                }
                Console.WriteLine();
            }
        }

        static List<string> ans = new List<string>();
        static bool[][] used = new bool[5][];
        static List<Tuple<string, List<Tuple<int, int>>>> an;
        static void find(Bor a, int x, int y, char[][] arr, string word, List<Tuple<int, int>> pair) {
            if (used[x][y])
                return;
            if (x > 5 || y > 5 || x < 0 || y < 0)
                return;
            if (a == null)
                return;
            if (a.term) {
                ans.Add(word);
                an.Add(Tuple.Create(word, pair));
            }
            //Console.WriteLine(word);
            used[x][y] = true;
            if (x + 1 < 5) {
                pair.Add(Tuple.Create(x + 1, y));
                find(a.a[arr[x + 1][y] - 'а'], x + 1, y, arr, word + arr[x + 1][y], pair);
                pair.RemoveAt(pair.Count - 1);
            }
            if (y + 1 < 5)
            {
                pair.Add(Tuple.Create(x, y + 1));
                find(a.a[arr[x][y + 1] - 'а'], x, y + 1, arr, word + arr[x][y + 1], pair);
                pair.RemoveAt(pair.Count - 1);
            }
            if (x - 1 >= 0)
            {
                pair.Add(Tuple.Create(x - 1, y));
                find(a.a[arr[x - 1][y] - 'а'], x - 1, y, arr, word + arr[x - 1][y], pair);
                pair.RemoveAt(pair.Count - 1);
            }
            if (y - 1 >= 0)
            {
                pair.Add(Tuple.Create(x, y - 1));
                find(a.a[arr[x][y - 1] - 'а'], x, y - 1, arr, word + arr[x][y - 1], pair);
                pair.RemoveAt(pair.Count - 1);
            }
            if (x + 1 < 5 && y + 1 < 5)
            {
                pair.Add(Tuple.Create(x + 1, y + 1));
                find(a.a[arr[x + 1][y + 1] - 'а'], x + 1, y + 1, arr, word + arr[x + 1][y + 1], pair);
                pair.RemoveAt(pair.Count - 1);
            }
            if (x + 1 < 5 && y - 1 >= 0)
            {
                pair.Add(Tuple.Create(x + 1, y - 1));
                find(a.a[arr[x + 1][y - 1] - 'а'], x + 1, y - 1, arr, word + arr[x + 1][y - 1], pair);
                pair.RemoveAt(pair.Count - 1);
            }
            if (x - 1 >= 0 && y + 1 < 5)
            {
                pair.Add(Tuple.Create(x - 1, y + 1));
                find(a.a[arr[x - 1][y + 1] - 'а'], x - 1, y + 1, arr, word + arr[x - 1][y + 1], pair);
                pair.RemoveAt(pair.Count - 1);
            }
            if (x - 1 >= 0 && y - 1 >= 0)
            {
                pair.Add(Tuple.Create(x - 1, y - 1));
                find(a.a[arr[x - 1][y - 1] - 'а'], x - 1, y - 1, arr, word + arr[x - 1][y - 1], pair);
                pair.RemoveAt(pair.Count - 1);
            }
            used[x][y] = false;
        }

        static void InitUsed() {
            for (int i = 0; i < 5; ++i) {
                used[i] = new bool[5];
                for (int j = 0; j < 5; ++j) {
                    used[i][j] = false;
                }
            }
        }
        static char[][] Kuda;
        static void Init() {
            InitUsed();
            System.IO.StreamReader file = new System.IO.StreamReader(@"Input.txt");
            List<string> lst = new List<string>();
            Dictionary<string, bool> map = new Dictionary<string, bool>();
            string q;
            int cnt = 0;
            while ((q = file.ReadLine()) != null)
            {
                string s = "";
                for (int i = 0; i < q.Length; ++i)
                {
                    if (q[i] == ' ')
                    {
                        //Console.WriteLine(s);
                        map[s] = true;
                        lst.Add(s);
                        s = "";
                        cnt++;
                    }
                    else
                    {
                        s += q[i];
                    }
                }

            }
            Console.WriteLine(cnt);
            file.Close();
            Bor root = new Bor();
            root = Solve(lst);
            string str;
            str = Console.ReadLine();
            char[][] arr = new char[5][];
            for (int i = 0; i < 5; ++i)
            {
                arr[i] = new char[5];
                for (int j = 0; j < 5; ++j)
                {
                    arr[i][j] = str[5 * i + j];
                }
            }
            print(arr);
            for (int i = 0; i < 5; ++i) {
                for (int j = 0; j < 5; ++j) {
                    InitUsed();
                    List<Tuple<int, int>> pair = new List<Tuple<int, int>>();
                    find(root.a[arr[i][j] - 'а'], i, j, arr, arr[i][j].ToString(), pair);
                }
            }
            Kuda = arr;
        }

        static bool cmp (string a, string b) {
            return a.Length > b.Length;
        }

        static void GetViewAnswer(List<string> a, char[][]arr) {
            char[][] b = new char[5][];
            for (int i = 0; i < 5; ++i) {
                b[i] = new char[5];
                for (int j = 0; j < 5; ++j)
                    b[i][j] = ' ';
            }
            for (int i = 0; i < a.Count; ++i) {
                for (int j = 0; j < an.Count; ++j) {
                    if(a[i] == an[j].Item1) {
                        for (int k = 0; k < an[j].Item2.Count; ++k) {
                            b[an[j].Item2[k].Item1][an[j].Item2[k].Item2] = arr[an[j].Item2[k].Item1][an[j].Item2[k].Item2];
                        }
                        break;
                    }
                }
                Console.WriteLine(a[i]);
                print(b);
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();

            }
        }

        static void Main(string[] args)
        {
            an = new List<Tuple<string, List<Tuple<int, int>>>>();
            Init();
            var set = new HashSet<string>();
            for (int i = 0; i < ans.Count; ++i) {
                set.Add(ans[i]);
            }
            ans = new List<string>();
            foreach(var i in set) {
                ans.Add(i);
            }
            for (int i = 0; i < ans.Count; ++i) {
                for (int j = i + 1; j < ans.Count; ++j) {
                    if(ans[i].Length < ans[j].Length) {
                        string tmp = "";
                        tmp = ans[j];
                        ans[j] = ans[i];
                        ans[i] = tmp;
                    }
                        
                }
            }
            for (int i = ans.Count - 1; i >= 0; --i) {
                Console.WriteLine(ans[i]);
            }
        }
    }
}
