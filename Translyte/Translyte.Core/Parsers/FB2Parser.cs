using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Translyte.Core.Models;

namespace Translyte.Core.Parsers
{
    //This class implements IParser interface and provides ability to 
    //read books of fb2 type.
    public class FB2Parser : IParser
    {
        //This function converts an FB2 book to book type Book. 
        //It can be either just book info or full book with content.
        public void Parse(ref Book book)
        {
            var xdoc = XDocument.Load(book.BookPath);
            if (xdoc.Root.Name.LocalName != "FictionBook")
                return;

            var nodes = xdoc.Root.Elements();
            var titleInfo = nodes.Where(x => x.Name.LocalName == "description").SingleOrDefault()
                .Elements().Where(x => x.Name.LocalName == "title-info").SingleOrDefault().Elements();

            book.Title = titleInfo.Where(x => x.Name.LocalName == "book-title").SingleOrDefault().Value;

            var author = titleInfo.Where(x => x.Name.LocalName == "author").SingleOrDefault();
            book.Author = Has("first-name", author) ?
                (author.Elements().Where(x => x.Name.LocalName == "first-name").SingleOrDefault().Value
                + (Has("middle-name", author) ? " " + author.Elements().Where(x => x.Name.LocalName == "middle-name").SingleOrDefault().Value : "")
                + " " + author.Elements().Where(x => x.Name.LocalName == "last-name").SingleOrDefault().Value) :
                (Has("nickname", author) ? (author.Elements().Where(x => x.Name.LocalName == "nickname").SingleOrDefault().Value) : "Unknown");

            book.Cover = Has("binary",nodes) ? nodes.Where(x => x.Name.LocalName == "binary").First().Value : "";
            
            book.ID = nodes.Where(x => x.Name.LocalName == "description").SingleOrDefault().Elements()
                .Where(x => x.Name.LocalName == "document-info").SingleOrDefault().Elements()
                .Where(x => x.Name.LocalName == "id").SingleOrDefault().Value;

            if (!book.GetType().Equals(typeof(BookFullModel)))
                return;
            ((BookFullModel)book).Annotation = Has("annotation", titleInfo)
                ? GetFormattedString(titleInfo.Where(x => x.Name.LocalName == "annotation").SingleOrDefault().Elements()) : "";
            ((BookFullModel)book).Language = titleInfo.Where(x => x.Name.LocalName == "lang").SingleOrDefault().Value;
            ((BookFullModel)book).Year = Has("date",titleInfo)?titleInfo.Where(x => x.Name.LocalName == "date").SingleOrDefault().Value:"";
            ((BookFullModel)book).Genres =
                new List<string>(titleInfo.Where(x => x.Name.LocalName == "genre").Select(x => GetGenre(x.Value)));
            ((BookFullModel)book).Chapters = new List<ChapterModel>(nodes.Where(x => x.Name.LocalName == "body").Elements()
                .Where(x => x.Name.LocalName == "section").Select(x => new ChapterModel()
                {
                    Title = Has("title", x) ?
                    GetFormattedString(x.Elements().Where(y => y.Name.LocalName == "title").SingleOrDefault().Elements()) : "",
                    Content = GetFormattedString(x.Elements())
                }));
        }
        //This function checks if tag contains another tag by name.
        private bool Has(string tagName, XElement tag)
        {
            return tag.Elements().Select(x => x.Name.LocalName).Contains(tagName);
        }
        //This function checks if tags contain tag by name.
        private bool Has(string tagName, IEnumerable<XElement> tags)
        {
            return tags.Select(x => x.Name.LocalName).Contains(tagName);
        }
        //This function add string formatting by xml tags.
        private string GetFormattedString(IEnumerable<XElement> elements)
        {
            string res = "";
            foreach (var el in elements)
            {
                switch (el.Name.LocalName)
                {
                    case "p":
                        if (el.HasElements)
                            res += GetFormattedString(el.Elements());
                        else
                            res += "\n\t" + el.Value; break;
                    case "empty-line": res += "\n\n"; break;
                    case "title": break;
                    //TODO: tags <cite>, <poem>, <image>
                    default:
                        if (el.HasElements)
                            res += GetFormattedString(el.Elements());
                        else
                            res += "\n\t" + el.Value; break;
                }
            }
            return res;
        }
        //This function returns language name by given code name.
        private string GetLanguage(string langCode)
        {
            switch (langCode)
            {
                case "abk": case "ab": return "Abkhazian"; 
                case "aze": case "az": return "Azerbaijan"; 
                case "alb": case "sqi": case "sq": return "Albanian"; 
                case "eng": case "en": return "English";
                case "arm": case "hye": case "hy": return"Armenian";
                case "BA": return "Bashkir";
                case "bel": case "be": return "Belarusian";
                case "bul": case "bg": return "Bulgarian";
                case "hun": case "hu": return "Hungarian";
                case "vie": case "vi": return "Vietnamese";
                case "dut": case "nla": case "nl": return "Dutch";
                case "ell": case "gre": case "el": return "Greek Modern (1453-)";
                case "dan": case "da": return "Daniysky";
                case "grc": return "Ancient Greek (until 1453)";
                case "heb": case "he": return "Hebrew";
                case "esl": case "spa": case "es": return "Spanish";
                case "ita": case "it": return "Italian"; 
                case "kaz": case "kk": return "Kazakh";
                case "kir": case "ky": return "Kirghiz";
                case "chi": case "zho": case "zh": return "Chinese";
                case "kor": case "ko": return "Korean";
                case "lat": case "la": return "Latin";
                case "lav": case "lv": return "Latvian";
                case "lit": case "lt": return "Lithuanian";
                case "mac": case "mak": case "mk": return "Makedoniysky";
                case "mol": case "mo": return "Moldavian";
                case "mon": case "mn": return "Mongolian";
                case "deu": case "ger": case "de": return "German";
                case "mul": return "Multiple language";
                case "und": return "Uncertain";
                case "nor": case "no": return "Norwegian";
                case "fas": case "per": case "fa": return "Persian";
                case "pol": case "pl": return "Polish";
                case "por": case "pt": return "Portuguese";
                case "rus": case "ru": return "Russian";
                case "san": case "sa": return "Sanskrit";
                case "slk": case "slo": case "sk": return "Slovak";
                case "slv": case "sl": return "Slovenian";
                case "tgk": case "tg": return "Tajik";
                case "tat": case "tt": return "Tatar";
                case "tur": case "tr": return "Turkish";
                case "uzb": case "uz": return "Uzbek";
                case "ukr": case "uk": return "Ukrainian";
                case "cym": case "wel": case "cy": return "Welsh";
                case "fin": case "fi": return "Finnish";
                case "fra": case "fre": case "fr": return "French";
                case "che": return "Chechen";
                case "ces": case "cze": case "cs": return "Czech";
                case "hr": return "Croatian";
                case "sve": case "swe": case "sv": return "Swedish";
                case "epo": case "eo": return "Esperanto";
                case "est": case "et": return "Estonian";
                case "jpn": case "ja": return "Japanese";
                default: return "Not set";
            }
        }
        //This function returns genre name by genre code.
        private string GetGenre(string genreCode)
        {
            switch (genreCode)
            {
                case "sf_history": return "Alternative history";
                case "sf_action": return "Fighting fantasy";
                case "sf_epic": return "Epic fantasy";
                case "sf_heroic": return "Heroic fantasy";
                case "sf_detective": return "Detective fiction";
                case "sf_cyberpunk": return "Cyberpunk";
                case "sf_space": return "Space fantasy";
                case "sf_social": return "Social fiction";
                case "sf_horror": return "Horror and mystery";
                case "sf_humor": return "Humorous fiction";
                case "sf_fantasy": return "Fantasy";
                case "sf": return "Science fiction";
                case "child_sf": return "Children's fiction";
                case "det_classic": return "Classic detective";
                case "det_police": return "Police detective";
                case "det_action": return "Militants";
                case "det_irony": return "Ironic detective";
                case "det_history": return "Historical detective";
                case "det_espionage": return "Spy detective";
                case "det_crime": return "Crime detective";
                case "det_political": return "Political thriller";
                case "det_maniac": return "Maniacs"; 
                case "det_hard": return "Cool detective";
                case "detective": return "Detective";
                case "thriller": return "Thriller";
                case "child_det": return "Kids action";
                case "caselove_detective": return "Action romance";
                case "prose": return "Prose";
                case "prose_classic": return "Classical prose";
                case "prose_history": return "Historical fiction";
                case "prose_contemporary": return "Modern prose";
                case "prose_counter": return "Counterculture";
                case "prose_rus_classic": return "Russian classical";
                case "prose_su_classics": return "Soviet classics";
                case "humor_prose": return "Humorous prose";
                case "love": return "Romance";
                case "love_contemporary": return "Contemporary romance";
                case "love_history": return "Historical romance";
                case "love_detective": return "Action romance";
                case "love_short": return "Short romance";
                case "love_erotica": return "Erotic";
                case "adv_western": return "Westerns";
                case "adv_history": return "Historical adventures";
                case "adv_indian": return "Adventure: Indians";
                case "adv_maritime": return "Marine adventures";
                case "adv_geo": return "Travel and geography";
                case "adv_animal": return "Nature and animals";
                case "adventure": return "Adventures: Other";
                case "child_adv": return "Kids adventures";
                case "children": return "Baby";
                case "child_tale": return "Tales";
                case "child_verse": return "Nursery rhymes"; 
                case "child_prose": return "Children prose";
                case "child_education": return "Children's educational books";
                case "poetry": return "Poetry";
                case "dramaturgy": return "Dramaturgy";
                case "humor_verse": return "Humorous poems";
                case "antique_ant": return "Ancient literature";
                case "antique_european": return "European ancient literature";
                case "antique_russian": return "Old russian literature";
                case "antique_east": return "Ancient oriental literature";
                case "antique_myths": return "Myths. Legends. Epic";
                case "antique": return "Ancient literature: Other";
                case "sci_history": return "History";
                case "sci_psychology": return "Psychology";
                case "sci_culture": return "Cultural";
                case "sci_religion": return "Religious";
                case "sci_philosophy": return "Philosophy";
                case "sci_politics": return "Policy";
                case "sci_business": return "Business books";
                case "sci_juris": return "Legal";
                case "sci_linguistic": return "Linguistics";
                case "sci_medicine" : return "Medicine";
                case "sci_phys": return "Physics";
                case "sci_math": return "Mathematics";
                case "sci_chem": return "Chemistry";
                case "sci_biology": return "Biology";
                case "sci_tech": return "Engineering";
                case "science": return "Science and education: Other";
                case "comp_www": return "Internet";
                case "comp_programming": return "Programming";
                case "comp_hard": return "Computer hardware";
                case "comp_soft": return "Program";
                case "comp_db":  return "Databases";
                case "comp_osnet": return "OS and network";
                case "computers": return "Computers: Other";
                case "ref_encyc": return "Encyclopedia";
                case "ref_dict": return "Dictionary";
                case "ref_ref": return "Directories";
                case "ref_guide": return "Manual";
                case "reference": return "Reference literature: Other";
                case "nonf_biography": return "Biographies and memoirs";
                case "nonf_publicism": return "Reading";
                case "nonf_criticism": return "Criticism";
                case "nonfiction": return "Documentary: Other";
                case "design": return "Art, design";
                case "religion": return "Religion";
                case "religion_rel": return "Religion";
                case "religion_esoterics": return "Esoteric";
                case "religion_self": return "Perfection";
                case "humor_anecdote": return "Jokes";
                case "humor": return "Humor";
                case "home_cooking": return "Cooking";
                case "home_pets": return "Pets";
                case "home_crafts": return "Hobbies, crafts";
                case "home_entertain": return "Entertainment";
                case "home_health": return "Health";
                case "home_garden": return "Garden";
                case "home_diy": return "DIY";
                case "home_sport": return "Sports";
                case "home_sex": return "Erotic sex";
                case "home": return "Home and family";
                default: return "Unknown";
            }
        }
    }
}
