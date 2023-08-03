using System.Data;
using Dapper;
using MySqlConnector;

namespace AlexGuitarsShop.Scripts;

public static class SeedDatabase
{
    public static void Init(string connectionString)
    {
        using IDbConnection db = new MySqlConnection(connectionString);
        InitGuitars(db);
        InitUsers(db);
        InitCartItems(db);
    }

    private static void InitGuitars(IDbConnection db)
    {
        db.Execute(@"CREATE TABLE IF NOT EXISTS Guitars
        (Id INT AUTO_INCREMENT NOT NULL,
        Name VARCHAR (50),
        Price INT,
        Description VARCHAR (600),
        Image LONGBLOB,
        IsDeleted TINYINT,
        PRIMARY KEY (Id));");
        if (db.ExecuteScalar<int>("SELECT COUNT(*) FROM guitars") == 0)
        {
            FillGuitars(db);
        }
    }
    
    private static void FillGuitars(IDbConnection db)
    {
        string currentPath = (Directory.GetCurrentDirectory() +".Scripts").Replace("\\", "\\\\");
        db.Execute($@"INSERT INTO Guitars 
        (Name, Price, Description, Image, IsDeleted)
        VALUES 
        ('GIBSON 57 LES PAUL GOLDTOP', 5000, 
         'Iconic electric guitar GIBSON 57 LES PAUL GOLDTOP DARKBACK 
         REISSUE VOS DOUBLE GOLD was a favorite instrument of many guitar heroes.',
         LOAD_FILE('{currentPath}\\products_images\\gibson-57-les-paul-goldtop.jpg'), 0),
        ('GIBSON ES-335 Figured Sixties Cherry', 3000, 
         'The GIBSON ES-335 FIGURED SIXTIES CHERRY semi-acoustic guitar is the perfect combination of shape and 
         functionality.',
         LOAD_FILE('{currentPath}\\products_images\\gibson-es-335-figured-sixties-cherry.jpg'), 0),
        ('GIBSON EXPLORER 70s CLASSIC White', 1500, 
         'Having been on many stages around the world, the icon of the 70s, 
         the GIBSON EXPLORER 70s CLASSIC WHITE electric guitar, has returned to a new generation of guitarists.',
         LOAD_FILE('{currentPath}\\products_images\\gibson-explorer-70s-classic-white.jpg'), 0),
        ('Gibson SG Standard Heritage Cherry', 1000,
         'The GIBSON SG STANDARD HERITAGE CHERRY is an electric guitar immortalized by Santana 
         at Woodstock and smashed on the Townsend stage. The GIBSON SG is a true rock icon.',
         LOAD_FILE('{currentPath}\\products_images\\gibson-sg-standard-heritage-cherry.jpg'), 0),
        ('GIBSON FIREBIRD CUSTOM Natural', 1200,
         'The GIBSON FIREBIRD CUSTOM EBONY adds an unmistakable vibe to your electric guitar collection. 
         At the time of its first release in the early 60s, less than a thousand pieces were made, 
         making this instrument a true rare bird.',
         LOAD_FILE('{currentPath}\\products_images\\gibson-firebird-custom-natural.jpg'), 0),
        ('FENDER AMERICAN PRO II TELECASTER', 1400,
         'The FENDER AMERICAN PRO II TELECASTER electric guitar draws on over seventy years of innovation, 
         inspiration and evolution to meet the needs of the modern player.',
         LOAD_FILE('{currentPath}\\products_images\\fender-american-pro-telecaster.jpg'), 0),
        ('FENDER NOVENTA JAZZMASTER', 1100,
         'Combining classic Fender style and dynamic single-coil pickups, 
         the Noventa series delivers powerful sound, modern playing capabilities and stylish looks.',
         LOAD_FILE('{currentPath}\\products_images\\fender-noventa-jazzmaster.jpg'), 0),
        ('Fender Player Stratocaster', 1000,
         'The inspiring sound of the Stratocaster is one of the foundations of Fender. 
         Featuring a classic electric guitar filled with authentic Fender feel and style.',
         LOAD_FILE('{currentPath}\\products_images\\fender-player-stratocaster.jpg'), 0),
        ('IBANEZ RG5121 BCF', 1250,
         'The Super Wizard HP 5-ply maple/wenge neck delivers super-smooth playability, 
         while the ebony fingerboard delivers tight lows and mids and signature high-end attack.',
         LOAD_FILE('{currentPath}\\products_images\\ibanez-rg5121-bcf.jpg'), 0),
        ('IBANEZ RG370AHMZ BMT', 800,
         'IBANEZ RG370AHMZ BMT is a 6-string electric guitar from the RG line. The model has amazing versatility. 
         It allows you to achieve amazing, very clear and clear sound when playing high-speed solos.',
         LOAD_FILE('{currentPath}\\products_images\\ibanez-rg370ahmz-bmt.jpg'), 0),
        ('IBANEZ RGMS8-BK', 800,
         'The Ibanez RGMS8 is an 8-string electric guitar from the popular RG series. 
         The instrument has a powerful, rich tone, which is important for conveying heavy guitar riffs 
         of non-standard electric guitar tuning.',
         LOAD_FILE('{currentPath}\\products_images\\ibanez-rgms8-bk.jpg'), 0),
        ('IBANEZ RG8520SLTD NTF', 900,
         'The IBANEZ RG8520SLTD NTF electric guitar is crafted by an elite group of highly skilled craftsmen 
         trained to make instruments of uncompromising quality.',
         LOAD_FILE('{currentPath}\\products_images\\ibanez-rg8520sltd-ntf.jpg'), 0),
        ('IBANEZ RG421 MOL', 380,
         'For more than twenty years, Ibanez RG guitars have proudly been the kings of metal guitars. 
         Ibanez RG guitars have evolved side by side with the very genre of metal they were intended for. ',
         LOAD_FILE('{currentPath}\\products_images\\ibanez-rg421-mol.jpg'), 0),
        ('Mayones Regius 6 Gothic', 2000,
         'An expressive version of the Regius series.This series presents high - quality,
         good - sounding electric guitars that excel in rock music.',
         LOAD_FILE('{currentPath}\\products_images\\mayones-regius-6-gothic.jpg'), 0),
        ('Mayones Regius PRO 6', 2500,
         'With great sound and looks, excellent construction and high - quality components,
         this is an instrument that you should definitely try for yourself.',
         LOAD_FILE('{currentPath}\\products_images\\mayones-regius-pro-6-gothic.jpg'), 0),
        ('CORT KX507 Multi Scale', 750,
         'The KX500MS, the first multi - scale guitar designed by Cort in 2018,
         has received worldwide recognition from thousands of heavy metal enthusiasts
         and progressive guitarists.',
         LOAD_FILE('{currentPath}\\products_images\\cort-kx507-multi-scale.jpg'), 0),
        ('CORT CR250', 400,
         'The Classic Rock series of electric guitars is all about the design,
         feel and performance of vintage Golden Era electric guitars that most players love so much.',
         LOAD_FILE('{currentPath}\\products_images\\cort-cr250.jpg'), 0),
        ('CORT KX-5', 195,
         'The KX Series is  for the thoroughly modern player who also appreciates
         the best of time - tested classic design and features.Double - cutaway design with easy access to
         the 24 - fret neck, the KX models are sonically as powerful as they look.',
         LOAD_FILE('{currentPath}\\products_images\\cort-kx-5.jpg'), 0),
        ('CORT X-2', 150,
         'Cort X - 2 is a versatile model that is ideal for any guitarist, whether
         it is an experienced person or a completely green beginner.
         Already with one appearance, the guitar says that it is intended for fans ofspeed.',
         LOAD_FILE('{currentPath}\\products_images\\cort-x-2.jpg'), 0),
        ('CORT X-5', 180,
         'X Series electric guitars are designed for speed. Aggressive body
         contours and a comfortable neck - to - body
         connection help you play quickly and easily.',
         LOAD_FILE('{currentPath}\\products_images\\cort-x-5.jpg'), 0),
        ('B.C.Rich WARCLOCK MK1', 1750,
         'The B.C.Rich Warlock 7 String Electric Guitar features hard tail construction
         with a satin natural painted neck for an easy, speedy
         feel.With fretwork and
         Playability unrivaled at their price point, the Mk1 Series bring an amazing
         level of quality to the entry level player.',
         LOAD_FILE('{currentPath}\\products_images\\bc-rich-warlock-mk1.jpg'), 0),
        ('B.C.Rich WARBEAST', 1750,
         'B.C.Rich guitars always stuck out of the crowd for their uniqueness
         and innovative solutions.
         With a body that combines the Warlock and Beast shapes, the Warbeast is an epically B.C.Rich style guitar.',
         LOAD_FILE('{currentPath}\\products_images\\bc-rich-warbeast.jpg'), 0),
        ('B.C.Rich IRONBIRD MK-2', 2800,
         'B.C.Rich guitars always stuck out of the crowd for their uniqueness
         and innovative solutions.This 50th Anniversary of the company brings another bold push toward 
         the future of modern guitar making.',
         LOAD_FILE('{currentPath}\\products_images\\bc-rich-ironbird-mk2.jpg'), 0),
        ('B.C.Rich MOCKINGBIRD', 1900,
         'The Mockingbird model was designed by B.C.Rich in 1976 and was popular
         from the moment of its release.The September 2010 issue of Guitar World ranked the Mockingbird
         as “the coolest guitar of all time, ” ahead of a number of models with a longer legacy.',
         LOAD_FILE('{currentPath}\\products_images\\bc-rich-mockinbird.jpg'), 0),
        ('BC RICH BICH', 3500,
         'This is a well-made unique 10 - string guitar for a highly affordable price.
         This guitar has features like neck - through - body construction, great sounding humbuckers,
         a unique 10 - string set up and a super comfortable neck.The guitar is also light-weight 
         adding to its comfort level.',
         LOAD_FILE('{currentPath}\\products_images\\bc-rich-bich.jpg'), 0),
        ('JACKSON JS32T Kelly', 500,
         'Fast, deadly and affordable, the JACKSON JS32T Kelly AH Viola Burst electric guitar takes a giant leap
         forward, making the classic Jackson sound, look and play accessible like never before.',
         LOAD_FILE('{currentPath}\\products_images\\jackson-js32t-kelly.jpg'), 0),
        ('JACKSON RR3 RHOADS', 1460,
         'The high - tech electric guitar continues the legacy of inspiring shredding guitars from around the world.
         This one combines a versatile tone with classic rock style and innovative technology to create a purebred
         guitar that proudly displays the deep Jackson pedigree.',
         LOAD_FILE('{currentPath}\\products_images\\jackson-rr3-roads.jpg'), 0),
        ('ESP E-II ARROW', 2370,
         'The E - II Standard Series are instruments designed and built by ESPs experienced Japanese luthiers
         that capture all of his soul and craftsmanship in creating the finest guitars and basses.',
         LOAD_FILE('{currentPath}\\products_images\\esp-e-2-arrow.jpg'), 0),
        ('ESP E-II ECLIPSE FT', 2400,
         'The E - II Eclipse is one of the most beautiful, loudest and highest quality singlecut guitars
         ever built in Japan at the ESP factory.',
         LOAD_FILE('{currentPath}\\products_images\\esp-e-2-eclipse-ft.jpg'), 0),
        ('LTD F-200', 700,
         'The guitars in the LTD 200 series offer great value and deliver higher quality
         than any other instrument in its price range.',
         LOAD_FILE('{currentPath}\\products_images\\ltd_f-200_black_satin.jpg'), 0);");
    }

    private static void InitUsers(IDbConnection db)
    {
        db.Execute(@"CREATE TABLE IF NOT EXISTS Users 
        (Id INT AUTO_INCREMENT NOT NULL,
         Name VARCHAR (15),
         Email VARCHAR (30) NOT NULL,
         Password VARCHAR (200),
         Role INT,
         PRIMARY KEY (Id));");
        if (db.ExecuteScalar<int>("SELECT COUNT(*) FROM Users") == 0)
        {
            FillUsers(db);
        }
    }

    private static void FillUsers(IDbConnection db)
    {
        db.Execute(@"INSERT INTO Users 
        (Name, Email, Password, Role)
        VALUES 
        ('Alex', 'lex95bond@gmail.com', 'f969fdbe811d8a66010d6f8973246763147a2a0914afc8087839e29b563a5af0', 2),
        ('Anton', 'Anton@gmail.com', '65e84be33532fb784c48129675f9eff3a682b27168c0ea744b2cf58ee02337c5', 0),
        ('Ben', 'Ben@gmail.com', '65e84be33532fb784c48129675f9eff3a682b27168c0ea744b2cf58ee02337c5', 0),
        ('Catrin', 'Catrin@gmail.com', '65e84be33532fb784c48129675f9eff3a682b27168c0ea744b2cf58ee02337c5', 0),
        ('Den', 'Den@gmail.com', '65e84be33532fb784c48129675f9eff3a682b27168c0ea744b2cf58ee02337c5', 0),
        ('Eric', 'Eric@gmail.com', '65e84be33532fb784c48129675f9eff3a682b27168c0ea744b2cf58ee02337c5', 0),
        ('Freddy', 'Freddy@gmail.com', '65e84be33532fb784c48129675f9eff3a682b27168c0ea744b2cf58ee02337c5', 0),
        ('Gven', 'Gven@gmail.com', '65e84be33532fb784c48129675f9eff3a682b27168c0ea744b2cf58ee02337c5', 0),
        ('Harry', 'Harry@gmail.com', '65e84be33532fb784c48129675f9eff3a682b27168c0ea744b2cf58ee02337c5', 0),
        ('John', 'John@gmail.com', '65e84be33532fb784c48129675f9eff3a682b27168c0ea744b2cf58ee02337c5', 0),
        ('Olga', 'Olga@gmail.com', '65e84be33532fb784c48129675f9eff3a682b27168c0ea744b2cf58ee02337c5', 0),
        ('Ned', 'Ned@gmail.com', '65e84be33532fb784c48129675f9eff3a682b27168c0ea744b2cf58ee02337c5', 0);");
    }

    private static void InitCartItems(IDbConnection db)
    {
        db.Execute(@"CREATE TABLE IF NOT EXISTS CartItems
        (
         Id INT AUTO_INCREMENT NOT NULL,
         Cart_Id VARCHAR (50) NOT NULL,
         Prod_Id INT NOT NULL,
         Quantity INT NOT NULL,
         PRIMARY KEY (Id)
        );");
    }
}