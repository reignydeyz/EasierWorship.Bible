using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasierWorship.Bible.Data
{
    public class Bible
    {
        #region Enums

        public enum Book
        {
            Default,
            Genesis,
            Exodus,
            Leviticus,
            Numbers,
            Deuteronomy,
            Joshua,
            Judges,
            Ruth,
            FirstSamuel,
            SecondSamuel,
            FirstKings,
            SecondKings,
            FirstChronicles,
            SecondChronicles,
            Ezra,
            Nehemiah,
            Esther,
            Job,
            Psalms,
            Proverbs,
            Ecclesiastes,
            SongOfSolomon,
            Isaiah,
            Jeremiah,
            Lamentations,
            Ezekiel,
            Daniel,
            Hosea,
            Joel,
            Amos,
            Obadiah,
            Jonah,
            Micah,
            Nahum,
            Habakkuk,
            Zephaniah,
            Haggai,
            Zechariah,
            Malachi,
            Matthew,
            Mark,
            Luke,
            John,
            Acts,
            Romans,
            FirstCorinthians,
            SecondCorinthians,
            Galatians,
            Ephesians,
            Philippians,
            Colossians,
            FirstThessalonians,
            SecondThessalonians,
            FirstTimothy,
            SecondTimothy,
            Titus,
            Philemon,
            Hebrews,
            James,
            FirstPeter,
            SecondPeter,
            FirstJohn,
            SecondJohn,
            ThirdJohn,
            Jude,
            Revelation
        }
        #endregion

        #region Properties
        public int b { get; set; }  // Book
        public int c { get; set; }  // Chapter
        public int v { get; set; }  // Verse
        public int t { get; set; }  // Text
        #endregion

        #region Constructor

        /// <summary>
        /// Sets ans opens database connection
        /// </summary>
        /// <param name="dbPath"></param>
        public Bible(string dbFilePath)
        {
            _dbFilePath = string.Format("Data Source={0};Version=3;", dbFilePath);

            _qry = "select book1.text from "
                    + "(select book.book as book, substr(verse, 1, pos - 1) + 0 as chapter, substr(substr(verse, pos + 1) || '000', 1, 3) + 0 as verse, book.unformatted as text "
                    + "from "
                    + "(select *, instr(verse, '.') as pos from verses) as book) as book1 ";

            _books = new Dictionary<Book, string>();
            _books.Add(Book.Genesis, "Gen");
            _books.Add(Book.Exodus, "Exod");
            _books.Add(Book.Leviticus, "Lev");
            _books.Add(Book.Numbers, "Num");
            _books.Add(Book.Deuteronomy, "Deut");
            _books.Add(Book.Joshua, "Josh");
            _books.Add(Book.Judges, "Judg");
            _books.Add(Book.Ruth, "Ruth");
            _books.Add(Book.FirstSamuel, "1Sam");
            _books.Add(Book.SecondSamuel, "2Sam");

            _books.Add(Book.FirstKings, "1Kgs");
            _books.Add(Book.SecondKings, "2Kgs");
            _books.Add(Book.FirstChronicles, "1Chr");
            _books.Add(Book.SecondChronicles, "2Chr");
            _books.Add(Book.Ezra, "Ezra");
            _books.Add(Book.Nehemiah, "Neh");
            _books.Add(Book.Esther, "Esth");
            _books.Add(Book.Job, "Job");
            _books.Add(Book.Psalms, "Ps");
            _books.Add(Book.Proverbs, "Prov");

            _books.Add(Book.Ecclesiastes, "Eccl");
            _books.Add(Book.SongOfSolomon, "Song");
            _books.Add(Book.Isaiah, "Isa");
            _books.Add(Book.Jeremiah, "Jer");
            _books.Add(Book.Lamentations, "Lam");
            _books.Add(Book.Ezekiel, "Ezek");
            _books.Add(Book.Daniel, "Dan");
            _books.Add(Book.Hosea, "Hos");
            _books.Add(Book.Joel, "Joel");
            _books.Add(Book.Amos, "Amos");

            _books.Add(Book.Obadiah, "Obad");
            _books.Add(Book.Jonah, "Jonah");
            _books.Add(Book.Micah, "Mic");
            _books.Add(Book.Nahum, "Nah");
            _books.Add(Book.Habakkuk, "Hab");
            _books.Add(Book.Zephaniah, "Zeph");
            _books.Add(Book.Haggai, "Hag");
            _books.Add(Book.Zechariah, "Zech");
            _books.Add(Book.Malachi, "Mal");
            _books.Add(Book.Matthew, "Matt");

            _books.Add(Book.Mark, "Mark");
            _books.Add(Book.Luke, "Luke");
            _books.Add(Book.John, "John");
            _books.Add(Book.Acts, "Acts");
            _books.Add(Book.Romans, "Rom");
            _books.Add(Book.FirstCorinthians, "1Cor");
            _books.Add(Book.SecondCorinthians, "2Cor");
            _books.Add(Book.Galatians, "Gal");
            _books.Add(Book.Ephesians, "Eph");
            _books.Add(Book.Philippians, "Phil");

            _books.Add(Book.Colossians, "Col");
            _books.Add(Book.FirstThessalonians, "1Thess");
            _books.Add(Book.SecondThessalonians, "2Thess");
            _books.Add(Book.FirstTimothy, "1Tim");
            _books.Add(Book.SecondTimothy, "2Tim");
            _books.Add(Book.Titus, "Titus");
            _books.Add(Book.Philemon, "Phlm");
            _books.Add(Book.Hebrews, "Heb");
            _books.Add(Book.James, "Jas");
            _books.Add(Book.FirstPeter, "1Pet");

            _books.Add(Book.SecondPeter, "2Pet");
            _books.Add(Book.FirstJohn, "1John");
            _books.Add(Book.SecondJohn, "2John");
            _books.Add(Book.ThirdJohn, "3John");
            _books.Add(Book.Jude, "Jude");
            _books.Add(Book.Revelation, "Rev");
        }

        #endregion

        #region Functions
        
        private readonly string _dbFilePath;
        private readonly Dictionary<Book, string> _books;
        private readonly string _qry;

        public string GetVerse(Book book, int chapter, int verse)
        {
            var qry = _qry + $"where book1.book = '{_books[book]}' and book1.chapter = @chapter and book1.verse = @verse";

            using (var conn = new SQLiteConnection(_dbFilePath))
            {
                return conn.Query<string>(qry, new { chapter, verse }).FirstOrDefault();
            }
        }

        public IEnumerable<string> GetVerses(Book book, int chapter, int fromVerse, int toVerse)
        {
            var qry = _qry + $"where book1.book = '{_books[book]}' and book1.chapter = @chapter and book1.verse >= @fromVerse and book1.verse <= @toVerse";

            using (var conn = new SQLiteConnection(_dbFilePath))
            {
                return conn.Query<string>(qry, new { chapter, fromVerse, toVerse });
            }
        }

        #endregion
    }
}
