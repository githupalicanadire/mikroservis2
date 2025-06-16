using Marten;
using Marten.Schema;
using Catalog.API.Models;

namespace Catalog.API.Data
{
    public class CatalogInitialData : IInitialData
    {
        public async Task Populate(IDocumentStore store, CancellationToken cancellation)
        {
            using var session = store.LightweightSession();

            if (await session.Query<Product>().AnyAsync())
                return;

            session.Store<Product>(GetPreconfiguredProducts());
            await session.SaveChangesAsync();
        }
        private static IEnumerable<Product> GetPreconfiguredProducts() => new List<Product>
        {
            new Product
            {
                Name = "Squid Game 5 Taş Oyunu",
                Description = "Squid Game 5 Taş Oyunu, Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/667d6_Squid_Game_5_Tas_Oyunu_.jpg",
                Price = 149.99M,
                Category = new List<string> { "Kutu Oyunları" }
            },
            new Product
            {
                Name = "Smile Games Günün Sorusu Kutu ...",
                Description = "Smile Games Günün Sorusu Kutu ..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/e802c_Smile_Games_Gunun_Sorusu_Kutu_Oyunu.jpg",
                Price = 274.99M,
                Category = new List<string> { "Kutu Oyunları" }
            },
            new Product
            {
                Name = "Smile Games Matematik Oyunu",
                Description = "Smile Games Matematik Oyunu, Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/e8c20_Smile_Games_Matematik_Oyunu.jpg",
                Price = 589.99M,
                Category = new List<string> { "Kutu Oyunları" }
            },
            new Product
            {
                Name = "UNO Eklenti Pak (Reverse Pack Jcv56)",
                Description = "UNO Eklenti Pak (Reverse Pack Jcv56), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/0ebd5_UNO_Eklenti_Paketleri_JCV55.jpg",
                Price = 119.99M,
                Category = new List<string> { "Kutu Oyunları" }
            },
            new Product
            {
                Name = "UNO Eklenti Pak (Stack Pack Jcv58)",
                Description = "UNO Eklenti Pak (Stack Pack Jcv58), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/3f171_UNO_Eklenti_Paketleri_JCV55.jpg",
                Price = 119.99M,
                Category = new List<string> { "Kutu Oyunları" }
            },
            new Product
            {
                Name = "UNO Eklenti Pak (Swap Pack Jcv59)",
                Description = "UNO Eklenti Pak (Swap Pack Jcv59), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/b1ba3_UNO_Eklenti_Paketleri_JCV55.jpg",
                Price = 119.99M,
                Category = new List<string> { "Kutu Oyunları" }
            },
            new Product
            {
                Name = "UNO Eklenti Pak (Speed Pack Jcv57)",
                Description = "UNO Eklenti Pak (Speed Pack Jcv57), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/c54b3_UNO_Eklenti_Paketleri_JCV55.jpg",
                Price = 119.99M,
                Category = new List<string> { "Kutu Oyunları" }
            },
            new Product
            {
                Name = "UNO Teams Kartlar Kart Oyunu H...",
                Description = "UNO Teams Kartlar Kart Oyunu H..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/8edb7_UNO_Teams_Kartlar_Kart_Oyunu_HXT58.jpg",
                Price = 319.99M,
                Category = new List<string> { "Kutu Oyunları" }
            },
            new Product
            {
                Name = "Smile Games Yumurta Suratlar K...",
                Description = "Smile Games Yumurta Suratlar K..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/58fc0_Smile_Games_Yumurta_Suratlar_Kutu_Oyunu.jpg",
                Price = 159.99M,
                Category = new List<string> { "Kutu Oyunları" }
            },
            new Product
            {
                Name = "Smile Games Sakın Düşme Kutu O...",
                Description = "Smile Games Sakın Düşme Kutu O..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/2c9de_Smile_Games_Sakin_Dusme_Kutu_Oyunu.jpg",
                Price = 169.99M,
                Category = new List<string> { "Kutu Oyunları" }
            },
            new Product
            {
                Name = "Smile Games Sudoku Zeka Oyunu",
                Description = "Smile Games Sudoku Zeka Oyunu, Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/4a165_Smile_Games_Sudoku_RD5284.jpg",
                Price = 399.99M,
                Category = new List<string> { "Kutu Oyunları" }
            },
            new Product
            {
                Name = "Duel Kutu Oyunu",
                Description = "Duel Kutu Oyunu, Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/779e6_Duel_Kutu_Oyunu.jpg",
                Price = 374.99M,
                Category = new List<string> { "Kutu Oyunları" }
            },
            new Product
            {
                Name = "Smile Games Megatown Türkiye K...",
                Description = "Smile Games Megatown Türkiye K..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/371fa_Smile_Games_Mega_Town_Turkiye_Kutu_Oyunu.jpg",
                Price = 459.99M,
                Category = new List<string> { "Kutu Oyunları" }
            },
            new Product
            {
                Name = "Smile Games Mini Basketbol Oyu...",
                Description = "Smile Games Mini Basketbol Oyu..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/df5df_Smile_Games_Mini_Basketbol_Oyunu.jpg",
                Price = 124.99M,
                Category = new List<string> { "Kutu Oyunları" }
            },
            new Product
            {
                Name = "Smile Games Pinball Kutu Oyunu",
                Description = "Smile Games Pinball Kutu Oyunu, Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/b8955_Smile_Games_Pinball_Kutu_Oyunu.jpg",
                Price = 199.99M,
                Category = new List<string> { "Kutu Oyunları" }
            },
            new Product
            {
                Name = "Smile Games Due Kart Oyunu",
                Description = "Smile Games Due Kart Oyunu, Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/4d4f9_Smile_Games_Due_Kart_Oyunu.jpg",
                Price = 114.99M,
                Category = new List<string> { "Kutu Oyunları" }
            },
            new Product
            {
                Name = "Smile Games Secret Words Kart ...",
                Description = "Smile Games Secret Words Kart ..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/d88a5_Smile_Games_Secret_Words_Kart_Oyunu.jpg",
                Price = 114.99M,
                Category = new List<string> { "Kutu Oyunları" }
            },
            new Product
            {
                Name = "Smile Games Pisa Tower Denge O...",
                Description = "Smile Games Pisa Tower Denge O..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/1cb69_Smile_Games_Pisa_Tower_Denge_Oyunu_.jpg",
                Price = 374.99M,
                Category = new List<string> { "Kutu Oyunları" }
            },
            new Product
            {
                Name = "Smile Games Rengini Bul Kutu O...",
                Description = "Smile Games Rengini Bul Kutu O..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/53eb3_Smile_Games_Rengini_Bul_Kutu_Oyunu.jpg",
                Price = 469.99M,
                Category = new List<string> { "Kutu Oyunları" }
            },
            new Product
            {
                Name = "Smile Games Çok Kelime Çok İşl...",
                Description = "Smile Games Çok Kelime Çok İşl..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/4ea74_Smile_Games_Cok_Kelime_Cok_Islem_Kutu_Oyunu.jpg",
                Price = 469.99M,
                Category = new List<string> { "Kutu Oyunları" }
            },
            new Product
            {
                Name = "Smile Games Domino Oyunu",
                Description = "Smile Games Domino Oyunu, Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/b9aae_Smile_Games_Domino_Oyunu.jpg",
                Price = 539.99M,
                Category = new List<string> { "Kutu Oyunları" }
            },
            new Product
            {
                Name = "Smile Games Becerikli Parmakla...",
                Description = "Smile Games Becerikli Parmakla..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/7a09b_Smile_Games_Becerikli_Parmaklar_Kutu_Oyunu.jpg",
                Price = 439.99M,
                Category = new List<string> { "Kutu Oyunları" }
            },
            new Product
            {
                Name = "Smile Games Trafik Zeka Oyunu",
                Description = "Smile Games Trafik Zeka Oyunu, Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/f0343_Smile_Games_Trafik_Zeka_Oyunu.jpg",
                Price = 399.99M,
                Category = new List<string> { "Kutu Oyunları" }
            },
            new Product
            {
                Name = "Smile Games Loop Loop Kutu Oyu...",
                Description = "Smile Games Loop Loop Kutu Oyu..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/25925_Smile_Games_Loop_Loop_Kutu_Oyunu.jpg",
                Price = 519.99M,
                Category = new List<string> { "Kutu Oyunları" }
            },
            new Product
            {
                Name = "Smile Games Balance Kutu Oyunu",
                Description = "Smile Games Balance Kutu Oyunu, Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/29ed5_Smile_Games_Balance_Kutu_Oyunu.jpg",
                Price = 539.99M,
                Category = new List<string> { "Kutu Oyunları" }
            },
            new Product
            {
                Name = "Smile Games Penta Blook Zeka v...",
                Description = "Smile Games Penta Blook Zeka v..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/07ea3_Smile_Games_Penta_Blook_Zeka_ve_Strateji_Oyunu.jpg",
                Price = 549.99M,
                Category = new List<string> { "Kutu Oyunları" }
            },
            new Product
            {
                Name = "Smile Games Kelime Oyunu RD531...",
                Description = "Smile Games Kelime Oyunu RD531..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/7f26c_Smile_Games_Kelime_Oyunu_RD5310.jpg",
                Price = 549.99M,
                Category = new List<string> { "Kutu Oyunları" }
            },
            new Product
            {
                Name = "Smile Games Nasıl Anlatsam Ses...",
                Description = "Smile Games Nasıl Anlatsam Ses..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/f5787_Smile_Games_Nasil_Anlatsam_Sessiz_Sinema_Kutu_Oyun.jpg",
                Price = 269.99M,
                Category = new List<string> { "Kutu Oyunları" }
            },
            new Product
            {
                Name = "Smile Games Trick Stick Kutu O...",
                Description = "Smile Games Trick Stick Kutu O..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/4e29a_Smile_Games_Trick_Stick_Kutu_Oyunu.jpg",
                Price = 309.99M,
                Category = new List<string> { "Kutu Oyunları" }
            },
            new Product
            {
                Name = "Smile Games Pizza Dilimlerim K...",
                Description = "Smile Games Pizza Dilimlerim K..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/e042d_Smile_Games_Pizza_Dilimlerim_Kutu_Oyunu.jpg",
                Price = 159.99M,
                Category = new List<string> { "Kutu Oyunları" }
            },
            new Product
            {
                Name = "Smile Games Rengim Nerede Kutu...",
                Description = "Smile Games Rengim Nerede Kutu..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/96331_Smile_Games_Rengim_Nerede_Kutu_Oyunu.jpg",
                Price = 99.99M,
                Category = new List<string> { "Kutu Oyunları" }
            },
            new Product
            {
                Name = "Smile Games Kafamda Ne Var Kut...",
                Description = "Smile Games Kafamda Ne Var Kut..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/9b52e_Smile_Games_Kafamda_Ne_Var_Kutu_Oyunu.jpg",
                Price = 194.99M,
                Category = new List<string> { "Kutu Oyunları" }
            },
            new Product
            {
                Name = "Smile Games Erken Okur Yazarlı...",
                Description = "Smile Games Erken Okur Yazarlı..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/81654_Smile_Games_Erken_Okur_Yazarlik_Kutu_Oyunu.jpg",
                Price = 234.99M,
                Category = new List<string> { "Kutu Oyunları" }
            },
            new Product
            {
                Name = "Smile Games Metropol Trafik Ku...",
                Description = "Smile Games Metropol Trafik Ku..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/51606_Smile_Games_Metropol_Trafik_Kutu_Oyunu.jpg",
                Price = 329.99M,
                Category = new List<string> { "Kutu Oyunları" }
            },
            new Product
            {
                Name = "Smile Games Button Clothes Kut...",
                Description = "Smile Games Button Clothes Kut..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/0c8ab_Smile_Games_Button_Clothes_Kutu_Oyunu.jpg",
                Price = 274.99M,
                Category = new List<string> { "Kutu Oyunları" }
            },
            new Product
            {
                Name = "Smile Games Junior Chef’s Kitc...",
                Description = "Smile Games Junior Chef’s Kitc..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/ac972_Smile_Games_Junior_Chef___s_Kitchen_Kutu_Oyunu.jpg",
                Price = 364.99M,
                Category = new List<string> { "Kutu Oyunları" }
            },
            new Product
            {
                Name = "Smile Games Zeka Çarkı Anaokul...",
                Description = "Smile Games Zeka Çarkı Anaokul..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/32e01_Smile_Games_Zeka_Carki_Anaokulu_Kutu_Oyunu.jpg",
                Price = 949.99M,
                Category = new List<string> { "Kutu Oyunları" }
            },
            new Product
            {
                Name = "Smile Games Word Boom Kutu Oyu...",
                Description = "Smile Games Word Boom Kutu Oyu..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/f4aed_Smile_Games_Word_Boom_Kutu_Oyunu.jpg",
                Price = 184.99M,
                Category = new List<string> { "Kutu Oyunları" }
            },
            new Product
            {
                Name = "Smile Games Magnetli Paper Duc...",
                Description = "Smile Games Magnetli Paper Duc..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/e28ab_Smile_Games_Magnetli_Paper_Duck_Kostum_Seti_.jpg",
                Price = 189.99M,
                Category = new List<string> { "Kutu Oyunları" }
            },
            new Product
            {
                Name = "Smile Games Magnetli Paper Duc...",
                Description = "Smile Games Magnetli Paper Duc..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/b18e7_Smile_Games_Magnetli_Paper_Duck_Moda_Seti_.jpg",
                Price = 194.99M,
                Category = new List<string> { "Kutu Oyunları" }
            },
            new Product
            {
                Name = "Smile Games Magnetli Paper Duc...",
                Description = "Smile Games Magnetli Paper Duc..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/51acc_Smile_Games_Magnetli_Paper_Duck_Uyku_Seti.jpg",
                Price = 199.99M,
                Category = new List<string> { "Kutu Oyunları" }
            },
            new Product
            {
                Name = "Smile Games Magnetli Paper Duc...",
                Description = "Smile Games Magnetli Paper Duc..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/30662_Smile_Games_Magnetli_Paper_Duck_Ev_Seti.jpg",
                Price = 329.99M,
                Category = new List<string> { "Kutu Oyunları" }
            },
            new Product
            {
                Name = "Smile Games Sayı Kulesi Kutu O...",
                Description = "Smile Games Sayı Kulesi Kutu O..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/d9445_Smile_Games_Sayi_Kulesi_Kutu_Oyunu.jpg",
                Price = 264.99M,
                Category = new List<string> { "Kutu Oyunları" }
            },
            new Product
            {
                Name = "Edu Sun Space Placement Kutu O...",
                Description = "Edu Sun Space Placement Kutu O..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/819a3_Edu_Sun_Space_Placement_Kutu_Oyunu.jpg",
                Price = 179.99M,
                Category = new List<string> { "Kutu Oyunları" }
            },
            new Product
            {
                Name = "Smile Games Kurbağa Denge Oyun...",
                Description = "Smile Games Kurbağa Denge Oyun..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/734a1_Smile_Games_Kurbaga_Denge_Oyunu_.jpg",
                Price = 219.99M,
                Category = new List<string> { "Kutu Oyunları" }
            },
            new Product
            {
                Name = "Smile Games Penguen Kulesi Den...",
                Description = "Smile Games Penguen Kulesi Den..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/b7cd2_Smile_Games_Penguen_Kulesi_Denge_Oyunu.jpg",
                Price = 319.99M,
                Category = new List<string> { "Kutu Oyunları" }
            },
            new Product
            {
                Name = "Smile Games Word To Go Kutu Oy...",
                Description = "Smile Games Word To Go Kutu Oy..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/fc97b_Smile_Games_Word_To_Go_Kutu_Oyunu.jpg",
                Price = 339.99M,
                Category = new List<string> { "Kutu Oyunları" }
            },
            new Product
            {
                Name = "Smile Games Word To Go XL Kutu...",
                Description = "Smile Games Word To Go XL Kutu..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/6e8cb_Smile_Games_Word_To_Go_XL_Kutu_Oyunu.jpg",
                Price = 519.00M,
                Category = new List<string> { "Kutu Oyunları" }
            },
            new Product
            {
                Name = "Bontempi Işıklı Mikrofonlu Ele...",
                Description = "Bontempi Işıklı Mikrofonlu Ele..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/1de79_Bontempi_Isikli_Mikrofonlu_Elektronik_Tabureli_Org.jpg",
                Price = 2699.00M,
                Category = new List<string> { "Müzik Aletleri" }
            },
            new Product
            {
                Name = "Bontempi Ayaklı Mikrofonlu Pem...",
                Description = "Bontempi Ayaklı Mikrofonlu Pem..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/31527_Bontempi_Ayakli_Mikrofonlu_Pembe_Elektronik_Cocuk_.jpg",
                Price = 1429.00M,
                Category = new List<string> { "Müzik Aletleri" }
            },
            new Product
            {
                Name = "Bontempi Ayaklı Mikrofonlu Kır...",
                Description = "Bontempi Ayaklı Mikrofonlu Kır..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/c65c0_Bontempi_Ayakli_Mikrofonlu_Kirmizi_Elektronik_Cocu.jpg",
                Price = 1429.00M,
                Category = new List<string> { "Müzik Aletleri" }
            },
            new Product
            {
                Name = "Akustik Çocuk G (Krem)",
                Description = "Akustik Çocuk G (Krem), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/a37b6_Akustik_Cocuk_Gitari_56_cm.jpg",
                Price = 449.99M,
                Category = new List<string> { "Müzik Aletleri" }
            },
            new Product
            {
                Name = "Akustik Çocuk G (Turuncu)",
                Description = "Akustik Çocuk G (Turuncu), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/90dfe_Akustik_Cocuk_Gitari_56_cm.jpg",
                Price = 449.99M,
                Category = new List<string> { "Müzik Aletleri" }
            },
            new Product
            {
                Name = "Hayvan Desenli  (Zürafa Desenli)",
                Description = "Hayvan Desenli  (Zürafa Desenli), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/72019_Hayvan_Desenli_Ukulele_55_cm.jpg",
                Price = 459.99M,
                Category = new List<string> { "Müzik Aletleri" }
            },
            new Product
            {
                Name = "Hayvan Desenli  (Gökkuşağı Desenli)",
                Description = "Hayvan Desenli  (Gökkuşağı Desenli), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/bcc93_Hayvan_Desenli_Ukulele_55_cm.jpg",
                Price = 459.99M,
                Category = new List<string> { "Müzik Aletleri" }
            },
            new Product
            {
                Name = "Funny Tune Eko  (Mavi)",
                Description = "Funny Tune Eko  (Mavi), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/42319_-.jpg",
                Price = 99.99M,
                Category = new List<string> { "Müzik Aletleri" }
            },
            new Product
            {
                Name = "Toy Tunes Şeffa (Mavi)",
                Description = "Toy Tunes Şeffa (Mavi), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/6c53c_Toy_Tunes_Seffaf_Blok_Flut.JPG",
                Price = 64.99M,
                Category = new List<string> { "Müzik Aletleri" }
            },
            new Product
            {
                Name = "Toy Tunes Şeffa (Sarı)",
                Description = "Toy Tunes Şeffa (Sarı), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/93e1d_Toy_Tunes_Seffaf_Blok_Flut.JPG",
                Price = 64.99M,
                Category = new List<string> { "Müzik Aletleri" }
            },
            new Product
            {
                Name = "Drum Rock Elektronik Bateri Se...",
                Description = "Drum Rock Elektronik Bateri Se..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/2cbf6_Drum_Rock_Elektronik_Bateri_Seti.jpg",
                Price = 2529.00M,
                Category = new List<string> { "Müzik Aletleri" }
            },
            new Product
            {
                Name = "Işıklı ve Müzik (Sarı)",
                Description = "Işıklı ve Müzik (Sarı), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/8124b_Isikli_ve_Muzikli_Kaya_Davul.jpg",
                Price = 349.99M,
                Category = new List<string> { "Müzik Aletleri" }
            },
            new Product
            {
                Name = "Meyveli Mini Gi (Kivi)",
                Description = "Meyveli Mini Gi (Kivi), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/2b44c_Meyveli_Mini_Gitar_.jpg",
                Price = 219.99M,
                Category = new List<string> { "Müzik Aletleri" }
            },
            new Product
            {
                Name = "Meyveli Mini Gi (Çilek)",
                Description = "Meyveli Mini Gi (Çilek), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/348af_Meyveli_Mini_Gitar_.jpg",
                Price = 219.99M,
                Category = new List<string> { "Müzik Aletleri" }
            },
            new Product
            {
                Name = "Meyveli Mini Gi (Karpuz)",
                Description = "Meyveli Mini Gi (Karpuz), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/4f0b9_Meyveli_Mini_Gitar_.jpg",
                Price = 219.99M,
                Category = new List<string> { "Müzik Aletleri" }
            },
            new Product
            {
                Name = "Meyveli Mini Gi (Portakal)",
                Description = "Meyveli Mini Gi (Portakal), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/c63c2_Meyveli_Mini_Gitar_.jpg",
                Price = 219.99M,
                Category = new List<string> { "Müzik Aletleri" }
            },
            new Product
            {
                Name = "Jazz Drum Mini  (Mavi)",
                Description = "Jazz Drum Mini  (Mavi), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/f0392_Jazz_Drum_Mini_Bateri_Seti.JPG",
                Price = 429.99M,
                Category = new List<string> { "Müzik Aletleri" }
            },
            new Product
            {
                Name = "Çocuk Müzik Aleti Seti",
                Description = "Çocuk Müzik Aleti Seti, Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/520df_Cocuk_Muzik_Aleti_Seti_.jpg",
                Price = 659.00M,
                Category = new List<string> { "Müzik Aletleri" }
            },
            new Product
            {
                Name = "Müzikli ve Işıklı Ayaklı Kırmı...",
                Description = "Müzikli ve Işıklı Ayaklı Kırmı..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/c359c_Muzikli_ve_Isikli_Ayakli_Kirmizi_Mikrofon_.jpg",
                Price = 1049.00M,
                Category = new List<string> { "Müzik Aletleri" }
            },
            new Product
            {
                Name = "Müzikli ve Işıklı Mikrofonlu E...",
                Description = "Müzikli ve Işıklı Mikrofonlu E..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/3efb1_Muzikli_ve_Isikli_Mikrofonlu_Elektronik_Tabureli_O.jpg",
                Price = 1789.00M,
                Category = new List<string> { "Müzik Aletleri" }
            },
            new Product
            {
                Name = "Işıklı Elektronik Kırmızı Org ...",
                Description = "Işıklı Elektronik Kırmızı Org ..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/b1624_Isikli_Elektronik_Kirmizi_Org_24_Tuslu_.jpg",
                Price = 839.00M,
                Category = new List<string> { "Müzik Aletleri" }
            },
            new Product
            {
                Name = "Müzikli ve Işıklı Ayaklı Pembe...",
                Description = "Müzikli ve Işıklı Ayaklı Pembe..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/df788_Muzikli_ve_Isikli_Ayakli_Pembe_Mikrofon_.jpg",
                Price = 879.00M,
                Category = new List<string> { "Müzik Aletleri" }
            },
            new Product
            {
                Name = "Müzikli ve Işıklı Mikrofon",
                Description = "Müzikli ve Işıklı Mikrofon, Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/d6a0d_Muzikli_ve_Isikli_Mikrofon_.jpg",
                Price = 579.00M,
                Category = new List<string> { "Müzik Aletleri" }
            },
            new Product
            {
                Name = "Bontempi Mikrofonlu Org 24 Tuş...",
                Description = "Bontempi Mikrofonlu Org 24 Tuş..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/7babd_Bontempi_Mikrofonlu_Org_24_Tuslu_.jpg",
                Price = 979.00M,
                Category = new List<string> { "Müzik Aletleri" }
            },
            new Product
            {
                Name = "Bontempi Pembe Elektronik Çocu...",
                Description = "Bontempi Pembe Elektronik Çocu..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/a7de8_Bontempi_Pembe_Elektronik_Cocuk_Rock_Gitari_53_cm.jpg",
                Price = 1049.00M,
                Category = new List<string> { "Müzik Aletleri" }
            },
            new Product
            {
                Name = "Bontempi Kırmızı Elektronik Ço...",
                Description = "Bontempi Kırmızı Elektronik Ço..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/ab08b_Bontempi_Kirmizi_Elektronik_Cocuk_Rock_Gitari_53_c.jpg",
                Price = 1049.00M,
                Category = new List<string> { "Müzik Aletleri" }
            },
            new Product
            {
                Name = "Akustik Çocuk G (Turuncu)",
                Description = "Akustik Çocuk G (Turuncu), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/07335_Akustik_Cocuk_Gitari_42_cm.jpg",
                Price = 249.99M,
                Category = new List<string> { "Müzik Aletleri" }
            },
            new Product
            {
                Name = "Akustik Çocuk G (Koyu Kahverengi)",
                Description = "Akustik Çocuk G (Koyu Kahverengi), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/95c57_Akustik_Cocuk_Gitari_42_cm.jpg",
                Price = 249.99M,
                Category = new List<string> { "Müzik Aletleri" }
            },
            new Product
            {
                Name = "Akustik Çocuk G (Açık Kahverengi)",
                Description = "Akustik Çocuk G (Açık Kahverengi), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/74497_Akustik_Cocuk_Gitari_42_cm.jpg",
                Price = 249.99M,
                Category = new List<string> { "Müzik Aletleri" }
            },
            new Product
            {
                Name = "Akustik Çocuk G (Krem)",
                Description = "Akustik Çocuk G (Krem), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/76830_Akustik_Cocuk_Gitari_42_cm.jpg",
                Price = 249.99M,
                Category = new List<string> { "Müzik Aletleri" }
            },
            new Product
            {
                Name = "Bontempi Işıklı Mini Elektroni...",
                Description = "Bontempi Işıklı Mini Elektroni..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/9196d_Bontempi_Isikli_Mini_Elektronik_Org_Mikrofonlu_Kar.jpg",
                Price = 979.00M,
                Category = new List<string> { "Müzik Aletleri" }
            },
            new Product
            {
                Name = "Akustik Çocuk G (Kahverengi)",
                Description = "Akustik Çocuk G (Kahverengi), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/04c58_Akustik_Cocuk_Gitari_56_cm.jpg",
                Price = 449.99M,
                Category = new List<string> { "Müzik Aletleri" }
            },
            new Product
            {
                Name = "Cubika 6 Notalı Ahşap Ksilofon...",
                Description = "Cubika 6 Notalı Ahşap Ksilofon..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/2ef0e_Woody_6_Notali_Ahsap_Ksilofon_.jpg",
                Price = 609.99M,
                Category = new List<string> { "Müzik Aletleri" }
            },
            new Product
            {
                Name = "Işıklı Trompet (Pembe)",
                Description = "Işıklı Trompet (Pembe), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/acc8c_Isikli_Trompet.jpg",
                Price = 519.00M,
                Category = new List<string> { "Müzik Aletleri" }
            },
            new Product
            {
                Name = "Toy Tunes Şeffa (Kırmızı)",
                Description = "Toy Tunes Şeffa (Kırmızı), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/641c5_Toy_Tunes_Seffaf_Blok_Flut.JPG",
                Price = 64.99M,
                Category = new List<string> { "Müzik Aletleri" }
            },
            new Product
            {
                Name = "Toy Tunes Şeffa (Mor)",
                Description = "Toy Tunes Şeffa (Mor), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/00e10_Toy_Tunes_Seffaf_Blok_Flut.JPG",
                Price = 64.99M,
                Category = new List<string> { "Müzik Aletleri" }
            },
            new Product
            {
                Name = "Toy Tunes Şeffa (Yeşil)",
                Description = "Toy Tunes Şeffa (Yeşil), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/6c53c_Toy_Tunes_Seffaf_Blok_Flut.JPG",
                Price = 64.99M,
                Category = new List<string> { "Müzik Aletleri" }
            },
            new Product
            {
                Name = "Akustik Gitar 6 (Kırmızı)",
                Description = "Akustik Gitar 6 (Kırmızı), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/ac483_Akustik_Gitar_69_cm..jpg",
                Price = 799.00M,
                Category = new List<string> { "Müzik Aletleri" }
            },
            new Product
            {
                Name = "Akustik Gitar 6 (Kahverengi)",
                Description = "Akustik Gitar 6 (Kahverengi), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/bd2fc_Akustik_Gitar_69_cm..jpg",
                Price = 799.00M,
                Category = new List<string> { "Müzik Aletleri" }
            },
            new Product
            {
                Name = "Akustik Keman 4 (Açık Kahverengi)",
                Description = "Akustik Keman 4 (Açık Kahverengi), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/701fb_Akustik_Keman_49_cm..JPG",
                Price = 529.00M,
                Category = new List<string> { "Müzik Aletleri" }
            },
            new Product
            {
                Name = "Akustik Keman 4 (Kırmızı)",
                Description = "Akustik Keman 4 (Kırmızı), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/8b2af_Akustik_Keman_49_cm..JPG",
                Price = 529.00M,
                Category = new List<string> { "Müzik Aletleri" }
            },
            new Product
            {
                Name = "Akustik Gitar 6 (Kırmızı)",
                Description = "Akustik Gitar 6 (Kırmızı), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/ae233_Akustik_Gitar_66_cm..jpg",
                Price = 599.00M,
                Category = new List<string> { "Müzik Aletleri" }
            },
            new Product
            {
                Name = "Akustik Gitar 6 (Kahverengi)",
                Description = "Akustik Gitar 6 (Kahverengi), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/14e3d_Akustik_Gitar_66_cm..jpg",
                Price = 599.00M,
                Category = new List<string> { "Müzik Aletleri" }
            },
            new Product
            {
                Name = "Elektronik Kema (Kırmızı)",
                Description = "Elektronik Kema (Kırmızı), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/81fdc_Elektronik_Keman_43_cm..jpg",
                Price = 359.99M,
                Category = new List<string> { "Müzik Aletleri" }
            },
            new Product
            {
                Name = "Mikrofonlu Elek (Siyah)",
                Description = "Mikrofonlu Elek (Siyah), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/e2f90_Mikrofonlu_Elektronik_Org_37_Tuslu.jpg",
                Price = 789.00M,
                Category = new List<string> { "Müzik Aletleri" }
            },
            new Product
            {
                Name = "Mikrofonlu Elek (Beyaz)",
                Description = "Mikrofonlu Elek (Beyaz), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/0456e_Mikrofonlu_Elektronik_Org_37_Tuslu.jpg",
                Price = 789.00M,
                Category = new List<string> { "Müzik Aletleri" }
            },
            new Product
            {
                Name = "Jazz Davul Set  (Yeşil)",
                Description = "Jazz Davul Set  (Yeşil), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/adb02_Jazz_Davul_Set_.jpg",
                Price = 1299.00M,
                Category = new List<string> { "Müzik Aletleri" }
            },
            new Product
            {
                Name = "Jazz Davul Set  (Kırmızı)",
                Description = "Jazz Davul Set  (Kırmızı), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/6858e_Jazz_Davul_Set_.jpg",
                Price = 1299.00M,
                Category = new List<string> { "Müzik Aletleri" }
            },
            new Product
            {
                Name = "Kristal Şehir Yolculuğu Yapım ...",
                Description = "Kristal Şehir Yolculuğu Yapım ..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/2e8da_Kristal_Sehir_Yolculugu_Yapim_Seti_240_Parca_.jpg",
                Price = 999.99M,
                Category = new List<string> { "Eğitici Oyuncaklar" }
            },
            new Product
            {
                Name = "Smile Games Flash Kart Yiyecek...",
                Description = "Smile Games Flash Kart Yiyecek..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/9ef96_Smile_Games_Flash_Kart_Yiyecekler_ve_Icecekler.jpg",
                Price = 59.99M,
                Category = new List<string> { "Eğitici Oyuncaklar" }
            },
            new Product
            {
                Name = "Smile Games Flash Kart Vücudum...",
                Description = "Smile Games Flash Kart Vücudum..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/d9e4b_Smile_Games_Flash_Kart_Vucudumuzu_Taniyalim.jpg",
                Price = 59.99M,
                Category = new List<string> { "Eğitici Oyuncaklar" }
            },
            new Product
            {
                Name = "Smile Games Flash Kart Sebzele...",
                Description = "Smile Games Flash Kart Sebzele..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/008d0_Smile_Games_Flash_Kart_Sebzeler.jpg",
                Price = 59.99M,
                Category = new List<string> { "Eğitici Oyuncaklar" }
            },
            new Product
            {
                Name = "Smile Games Flash Kart Saatler...",
                Description = "Smile Games Flash Kart Saatler..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/f1cd8_Smile_Games_Flash_Kart_Saatler_.jpg",
                Price = 59.99M,
                Category = new List<string> { "Eğitici Oyuncaklar" }
            },
            new Product
            {
                Name = "Smile Games Flash Kart Renkler",
                Description = "Smile Games Flash Kart Renkler, Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/2ede1_Smile_Games_Flash_Kart_Renkler.jpg",
                Price = 59.99M,
                Category = new List<string> { "Eğitici Oyuncaklar" }
            },
            new Product
            {
                Name = "Smile Games Eğlence Küpüm",
                Description = "Smile Games Eğlence Küpüm, Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/f27d0_Smile_Games_Eglence_Kupum_.jpg",
                Price = 354.99M,
                Category = new List<string> { "Eğitici Oyuncaklar" }
            },
            new Product
            {
                Name = "Marvel Spiderman İngilizce Tür...",
                Description = "Marvel Spiderman İngilizce Tür..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/efc6a_Marvel_Spiderman_Ingilizce_Turkce_Laptop.jpg",
                Price = 2799.99M,
                Category = new List<string> { "Eğitici Oyuncaklar" }
            },
            new Product
            {
                Name = "PAW Patrol İngilizce Türkçe La...",
                Description = "PAW Patrol İngilizce Türkçe La..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/8861b_PAW_Patrol_Ingilizce_Turkce_Laptop.jpg",
                Price = 2799.99M,
                Category = new List<string> { "Eğitici Oyuncaklar" }
            },
            new Product
            {
                Name = "Fisher Price Matematikçi Timsa...",
                Description = "Fisher Price Matematikçi Timsa..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/da4da_Fisher_Price_Matematikci_Timsah_JCT13.jpg",
                Price = 1399.99M,
                Category = new List<string> { "Eğitici Oyuncaklar" }
            },
            new Product
            {
                Name = "Manyetik Aktivite Hayvanlar 54...",
                Description = "Manyetik Aktivite Hayvanlar 54..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/88de2_Manyetik_Aktivite_Hayvanlar_54_Parca.jpg",
                Price = 249.99M,
                Category = new List<string> { "Eğitici Oyuncaklar" }
            },
            new Product
            {
                Name = "Manyetik Aktivite Kız Kıyafet ...",
                Description = "Manyetik Aktivite Kız Kıyafet ..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/445e4_Manyetik_Aktivite_Kiz_Kiyafet_Giydirme_Oyunu_77_Pa.jpg",
                Price = 249.99M,
                Category = new List<string> { "Eğitici Oyuncaklar" }
            },
            new Product
            {
                Name = "Smile Games Lab (Mavi)",
                Description = "Smile Games Lab (Mavi), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/73460_Smile_Games_Labirent_Disk_Zeka_Oyunu.jpg",
                Price = 229.99M,
                Category = new List<string> { "Eğitici Oyuncaklar" }
            },
            new Product
            {
                Name = "Smile Games Labirent Top Zeka ...",
                Description = "Smile Games Labirent Top Zeka ..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/93f7d_Smile_Games_Labirent_Top_Zeka_Oyunu.jpg",
                Price = 269.99M,
                Category = new List<string> { "Eğitici Oyuncaklar" }
            },
            new Product
            {
                Name = "Crafy İlk Resmim Etkinlik Seti",
                Description = "Crafy İlk Resmim Etkinlik Seti, Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/6c615_Crafy_Ilk_Resmim_Etkinlik_Seti.jpg",
                Price = 169.99M,
                Category = new List<string> { "Eğitici Oyuncaklar" }
            },
            new Product
            {
                Name = "Rubik's Zeka Küpü 3x3",
                Description = "Rubik's Zeka Küpü 3x3, Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/1c7db_Rubik_s_Zeka_Kupu_3x3.jpg",
                Price = 469.99M,
                Category = new List<string> { "Eğitici Oyuncaklar" }
            },
            new Product
            {
                Name = "Funmix Sanal Be (Sarı)",
                Description = "Funmix Sanal Be (Sarı), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/0d28c_Funmix_Sanal_Bebek_.jpg",
                Price = 129.99M,
                Category = new List<string> { "Eğitici Oyuncaklar" }
            },
            new Product
            {
                Name = "Funmix Sanal Be (Pembe)",
                Description = "Funmix Sanal Be (Pembe), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/a2a88_Funmix_Sanal_Bebek_.jpg",
                Price = 129.99M,
                Category = new List<string> { "Eğitici Oyuncaklar" }
            },
            new Product
            {
                Name = "Funmix Sanal Be (Kırmızı)",
                Description = "Funmix Sanal Be (Kırmızı), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/3cfd7_Funmix_Sanal_Bebek_.jpg",
                Price = 129.99M,
                Category = new List<string> { "Eğitici Oyuncaklar" }
            },
            new Product
            {
                Name = "Funmix Sanal Be (Mavi)",
                Description = "Funmix Sanal Be (Mavi), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/cf576_Funmix_Sanal_Bebek_.jpg",
                Price = 129.99M,
                Category = new List<string> { "Eğitici Oyuncaklar" }
            },
            new Product
            {
                Name = "Funmix Kalp Şek (Sarı)",
                Description = "Funmix Kalp Şek (Sarı), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/e8cb9_Funmix_Kalp_Sekilli_Sanal_Bebek_.jpg",
                Price = 129.99M,
                Category = new List<string> { "Eğitici Oyuncaklar" }
            },
            new Product
            {
                Name = "Funmix Kalp Şek (Mavi)",
                Description = "Funmix Kalp Şek (Mavi), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/bdd49_Funmix_Kalp_Sekilli_Sanal_Bebek_.jpg",
                Price = 129.99M,
                Category = new List<string> { "Eğitici Oyuncaklar" }
            },
            new Product
            {
                Name = "Funmix Kalp Şek (Kırmızı)",
                Description = "Funmix Kalp Şek (Kırmızı), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/a9b13_Funmix_Kalp_Sekilli_Sanal_Bebek_.jpg",
                Price = 129.99M,
                Category = new List<string> { "Eğitici Oyuncaklar" }
            },
            new Product
            {
                Name = "Funmix Kalp Şek (Pembe)",
                Description = "Funmix Kalp Şek (Pembe), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/6e122_Funmix_Kalp_Sekilli_Sanal_Bebek_.jpg",
                Price = 129.99M,
                Category = new List<string> { "Eğitici Oyuncaklar" }
            },
            new Product
            {
                Name = "Manyetik Uzay Roketi 55 Parça",
                Description = "Manyetik Uzay Roketi 55 Parça, Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/f7c39_Manyetik_Uzay_Roketi_55_Parca.jpg",
                Price = 799.99M,
                Category = new List<string> { "Eğitici Oyuncaklar" }
            },
            new Product
            {
                Name = "Smile Games Manyetik Küp",
                Description = "Smile Games Manyetik Küp, Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/29532_Smile_Games_Manyetik_Kup_.jpg",
                Price = 599.99M,
                Category = new List<string> { "Eğitici Oyuncaklar" }
            },
            new Product
            {
                Name = "Smile Games 3D Basketbol Parma...",
                Description = "Smile Games 3D Basketbol Parma..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/76d78_Smile_Games_3D_Basketbol_Parmak_Topu_.jpg",
                Price = 79.99M,
                Category = new List<string> { "Eğitici Oyuncaklar" }
            },
            new Product
            {
                Name = "Smile Games Sih (Pembe Yıldız)",
                Description = "Smile Games Sih (Pembe Yıldız), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/2dd34_Smile_Games_Sihirli_Sekilli_Renkli_Daire.jpg",
                Price = 99.99M,
                Category = new List<string> { "Eğitici Oyuncaklar" }
            },
            new Product
            {
                Name = "Smile Games Sih (Yeşil Yıldız)",
                Description = "Smile Games Sih (Yeşil Yıldız), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/21e63_Smile_Games_Sihirli_Sekilli_Renkli_Daire.jpg",
                Price = 99.99M,
                Category = new List<string> { "Eğitici Oyuncaklar" }
            },
            new Product
            {
                Name = "Smile Games Sih (Turuncu Altıgen)",
                Description = "Smile Games Sih (Turuncu Altıgen), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/fda44_Smile_Games_Sihirli_Sekilli_Renkli_Daire.jpg",
                Price = 99.99M,
                Category = new List<string> { "Eğitici Oyuncaklar" }
            },
            new Product
            {
                Name = "Smile Games Sih (Sarı Altıgen)",
                Description = "Smile Games Sih (Sarı Altıgen), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/d65f4_Smile_Games_Sihirli_Sekilli_Renkli_Daire.jpg",
                Price = 99.99M,
                Category = new List<string> { "Eğitici Oyuncaklar" }
            },
            new Product
            {
                Name = "Smile Games Sihirli Renkli Dön...",
                Description = "Smile Games Sihirli Renkli Dön..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/ded25_Smile_Games_Sihirli_Renkli_Dongu.jpg",
                Price = 79.99M,
                Category = new List<string> { "Eğitici Oyuncaklar" }
            },
            new Product
            {
                Name = "Smile Games Renkli Döngü Zeka ...",
                Description = "Smile Games Renkli Döngü Zeka ..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/c26c8_Smile_Games_Renkli_Dongu_Zeka_Oyunu.jpg",
                Price = 239.99M,
                Category = new List<string> { "Eğitici Oyuncaklar" }
            },
            new Product
            {
                Name = "Crafy Racing Oyun Kumu Seti 25...",
                Description = "Crafy Racing Oyun Kumu Seti 25..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/f5d68_Crafy_Racing_Oyun_Kumu_Seti_250_g_13_Parca.jpg",
                Price = 289.99M,
                Category = new List<string> { "Eğitici Oyuncaklar" }
            },
            new Product
            {
                Name = "Crafy 12’li Oyun Hamuru Kalıp ...",
                Description = "Crafy 12’li Oyun Hamuru Kalıp ..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/1b2eb_Crafy_12___li_Oyun_Hamuru_Kalip_Seti_____Yaraticil.jpg",
                Price = 99.99M,
                Category = new List<string> { "Eğitici Oyuncaklar" }
            },
            new Product
            {
                Name = "Crafy 14’lü Oyun Hamuru Kalıp ...",
                Description = "Crafy 14’lü Oyun Hamuru Kalıp ..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/37b6f_Crafy_14___lu_Oyun_Hamuru_Kalip_Seti_____Dinozorla.jpg",
                Price = 99.99M,
                Category = new List<string> { "Eğitici Oyuncaklar" }
            },
            new Product
            {
                Name = "Gloopy Islanmayan Sihirli Kum ...",
                Description = "Gloopy Islanmayan Sihirli Kum ..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/a82a5_Gloopy_Islanmayan_Sihirli_Kum_Seti.jpg",
                Price = 349.99M,
                Category = new List<string> { "Eğitici Oyuncaklar" }
            },
            new Product
            {
                Name = "Smile Games Sevimli Hayvanlar ...",
                Description = "Smile Games Sevimli Hayvanlar ..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/b98af_Smile_Games_Sevimli_Hayvanlar_Ip_Baglama_Oyunu.jpg",
                Price = 144.99M,
                Category = new List<string> { "Eğitici Oyuncaklar" }
            },
            new Product
            {
                Name = "Smile Games Yüksek Kontrastlı ...",
                Description = "Smile Games Yüksek Kontrastlı ..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/30764_Smile_Games_Yuksek_Kontrastli_Bebek_Hafiza_Kartlar.jpg",
                Price = 99.99M,
                Category = new List<string> { "Eğitici Oyuncaklar" }
            },
            new Product
            {
                Name = "Sesli ve Işıklı (Kırmızı)",
                Description = "Sesli ve Işıklı (Kırmızı), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/cb05c_Sesli_ve_Isikli_Carp_Don_Sevimli_Tospik_Egitici_Ka.jpg",
                Price = 299.99M,
                Category = new List<string> { "Eğitici Oyuncaklar" }
            },
            new Product
            {
                Name = "Sesli ve Işıklı (Sarı)",
                Description = "Sesli ve Işıklı (Sarı), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/691b2_Sesli_ve_Isikli_Carp_Don_Sevimli_Tospik_Egitici_Ka.jpg",
                Price = 299.99M,
                Category = new List<string> { "Eğitici Oyuncaklar" }
            },
            new Product
            {
                Name = "Sesli ve Işıklı (Yeşil)",
                Description = "Sesli ve Işıklı (Yeşil), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/ea71b_Sesli_ve_Isikli_Carp_Don_Sevimli_Tospik_Egitici_Ka.jpg",
                Price = 299.99M,
                Category = new List<string> { "Eğitici Oyuncaklar" }
            },
            new Product
            {
                Name = "Sesli ve Işıklı (Mavi)",
                Description = "Sesli ve Işıklı (Mavi), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/e2274_Sesli_ve_Isikli_Carp_Don_Sevimli_Tospik_Egitici_Ka.jpg",
                Price = 299.99M,
                Category = new List<string> { "Eğitici Oyuncaklar" }
            },
            new Product
            {
                Name = "Otobant Eğitici ve Eğlenceli Y...",
                Description = "Otobant Eğitici ve Eğlenceli Y..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/826ff_Otobant_Egitici_ve_Eglenceli_Yol_Yapim_Bandi_10_me.jpg",
                Price = 119.99M,
                Category = new List<string> { "Eğitici Oyuncaklar" }
            },
            new Product
            {
                Name = "Crafy Dr. Toothy Oyun Hamuru S...",
                Description = "Crafy Dr. Toothy Oyun Hamuru S..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/5a038_Crafy_Dr._Toothy_Oyun_Hamuru_Seti_15_Parca.jpg",
                Price = 729.99M,
                Category = new List<string> { "Eğitici Oyuncaklar" }
            },
            new Product
            {
                Name = "Crafy Jr. Toothy Oyun Hamuru S...",
                Description = "Crafy Jr. Toothy Oyun Hamuru S..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/37eb8_Crafy_Jr._Toothy_Oyun_Hamuru_Seti_12_Parca.jpg",
                Price = 489.99M,
                Category = new List<string> { "Eğitici Oyuncaklar" }
            },
            new Product
            {
                Name = "Rubik's Anahtarlıklı Zeka Küpü",
                Description = "Rubik's Anahtarlıklı Zeka Küpü, Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/3e4ed_Rubiks_Anahtarlikli_Zeka_Kupu.jpg",
                Price = 519.99M,
                Category = new List<string> { "Eğitici Oyuncaklar" }
            },
            new Product
            {
                Name = "Rubik's Yarış Oyunu",
                Description = "Rubik's Yarış Oyunu, Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/c030c_Rubiks_Yaris_Oyunu.jpg",
                Price = 1119.00M,
                Category = new List<string> { "Eğitici Oyuncaklar" }
            },
            new Product
            {
                Name = "Sesli Disney Angel Real Fx Ele...",
                Description = "Sesli Disney Angel Real Fx Ele..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/9714f_Sesli_Disney_Angel_Real_Fx_Elektronik_Kukla_30_cm.jpg",
                Price = 4289.99M,
                Category = new List<string> { "Peluş Oyuncaklar" }
            },
            new Product
            {
                Name = "Sesli Disney Angel Real Fx Ele...",
                Description = "Sesli Disney Angel Real Fx Ele..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/6078c_Sesli_Disney_Angel_Real_Fx_Elektronik_Kukla_45_cm.jpg",
                Price = 6989.99M,
                Category = new List<string> { "Peluş Oyuncaklar" }
            },
            new Product
            {
                Name = "Sesli Disney Stitch Real Fx El...",
                Description = "Sesli Disney Stitch Real Fx El..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/65d38_Sesli_Disney_Stitch_Real_Fx_Elektronik_Kukla_30_cm.jpg",
                Price = 4289.99M,
                Category = new List<string> { "Peluş Oyuncaklar" }
            },
            new Product
            {
                Name = "Sesli Disney Stitch Real Fx El...",
                Description = "Sesli Disney Stitch Real Fx El..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/ab040_Sesli_Disney_Stitch_Real_Fx_Elektronik_Kukla_45_cm.jpg",
                Price = 6989.99M,
                Category = new List<string> { "Peluş Oyuncaklar" }
            },
            new Product
            {
                Name = "Sesli Inside Ou (Kaygı)",
                Description = "Sesli Inside Ou (Kaygı), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/a896f_Sesli_Inside_Out_2_Mini_Pelus_NDN01000.jpg",
                Price = 1199.99M,
                Category = new List<string> { "Peluş Oyuncaklar" }
            },
            new Product
            {
                Name = "Sesli Inside Ou (Neşe)",
                Description = "Sesli Inside Ou (Neşe), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/cc6df_Sesli_Inside_Out_2_Mini_Pelus_NDN01000.jpg",
                Price = 1199.99M,
                Category = new List<string> { "Peluş Oyuncaklar" }
            },
            new Product
            {
                Name = "Sesli Inside Ou (Üzüntü)",
                Description = "Sesli Inside Ou (Üzüntü), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/89e6f_Sesli_Inside_Out_2_Mini_Pelus_NDN01000.jpg",
                Price = 1199.99M,
                Category = new List<string> { "Peluş Oyuncaklar" }
            },
            new Product
            {
                Name = "Sesli ve Hareketli Inside Out ...",
                Description = "Sesli ve Hareketli Inside Out ..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/086ea_Sesli_ve_Hareketli_Inside_Out_2_Kaygi_Pelus_NDN030.jpg",
                Price = 2499.99M,
                Category = new List<string> { "Peluş Oyuncaklar" }
            },
            new Product
            {
                Name = "Inside Out 2 Ağırlaştırılmış Ü...",
                Description = "Inside Out 2 Ağırlaştırılmış Ü..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/0d946_Inside_Out_2_Agirlastirilmis_Uzuntu_Pelus_NDN02000.jpg",
                Price = 1999.99M,
                Category = new List<string> { "Peluş Oyuncaklar" }
            },
            new Product
            {
                Name = "Inside Out 2 Ağırlaştırılmış U...",
                Description = "Inside Out 2 Ağırlaştırılmış U..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/aaf91_Inside_Out_2_Agirlastirilmis_Utanc_Pelus_NDN00000.jpg",
                Price = 2499.99M,
                Category = new List<string> { "Peluş Oyuncaklar" }
            },
            new Product
            {
                Name = "Adopt Me Peluş  (Triceratops)",
                Description = "Adopt Me Peluş  (Triceratops), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/17852_Adopt_Me_Pelus_20_cm_S4_ADT04000.jpg",
                Price = 1199.99M,
                Category = new List<string> { "Peluş Oyuncaklar" }
            },
            new Product
            {
                Name = "Adopt Me Peluş  (Puffin)",
                Description = "Adopt Me Peluş  (Puffin), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/a77fb_Adopt_Me_Pelus_20_cm_S4_ADT04000.jpg",
                Price = 1199.99M,
                Category = new List<string> { "Peluş Oyuncaklar" }
            },
            new Product
            {
                Name = "Adopt Me Peluş  (Shiba Inu)",
                Description = "Adopt Me Peluş  (Shiba Inu), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/fa7b1_Adopt_Me_Pelus_20_cm_S4_ADT04000.jpg",
                Price = 1199.99M,
                Category = new List<string> { "Peluş Oyuncaklar" }
            },
            new Product
            {
                Name = "Adopt Me Peluş  (Dragon)",
                Description = "Adopt Me Peluş  (Dragon), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/d24af_Adopt_Me_Pelus_20_cm_S4_ADT04000.jpg",
                Price = 1199.99M,
                Category = new List<string> { "Peluş Oyuncaklar" }
            },
            new Product
            {
                Name = "Adopt Me Peluş  (Turtle)",
                Description = "Adopt Me Peluş  (Turtle), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/10626_Adopt_Me_Pelus_20_cm_S4_ADT04000.jpg",
                Price = 1199.99M,
                Category = new List<string> { "Peluş Oyuncaklar" }
            },
            new Product
            {
                Name = "Adopt Me Peluş  (Maneki-Neko)",
                Description = "Adopt Me Peluş  (Maneki-Neko), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/b1aa3_Adopt_Me_Pelus_20_cm_S4_ADT04000.jpg",
                Price = 1199.99M,
                Category = new List<string> { "Peluş Oyuncaklar" }
            },
            new Product
            {
                Name = "Baby Paws Yummy Chihuahua Pelu...",
                Description = "Baby Paws Yummy Chihuahua Pelu..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/2eb2b_Baby_Paws_Yummy_Chihuahua_Pelus_BAW03100.jpg",
                Price = 1199.99M,
                Category = new List<string> { "Peluş Oyuncaklar" }
            },
            new Product
            {
                Name = "Baby Paws Yummy German Shepher...",
                Description = "Baby Paws Yummy German Shepher..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/16c5c_Baby_Paws_Yummy_German_Shepherd_Pelus_BAW03200_.jpg",
                Price = 1199.99M,
                Category = new List<string> { "Peluş Oyuncaklar" }
            },
            new Product
            {
                Name = "Baby Paws Yummy Shiba Inu Pelu...",
                Description = "Baby Paws Yummy Shiba Inu Pelu..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/177c5_Baby_Paws_Yummy_Shiba_Inu_Pelus_BAW03300.jpg",
                Price = 1199.99M,
                Category = new List<string> { "Peluş Oyuncaklar" }
            },
            new Product
            {
                Name = "Sesli Baby Paws Tavşan Peluş B...",
                Description = "Sesli Baby Paws Tavşan Peluş B..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/f42f5_Sesli_Baby_Paws_Tavsan_Pelus_BAW02000.jpg",
                Price = 1699.99M,
                Category = new List<string> { "Peluş Oyuncaklar" }
            },
            new Product
            {
                Name = "Sesli Baby Paws Kedi Peluş BAW...",
                Description = "Sesli Baby Paws Kedi Peluş BAW..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/1f998_Sesli_Baby_Paws_Kedi_Pelus_BAW05000.jpg",
                Price = 1599.99M,
                Category = new List<string> { "Peluş Oyuncaklar" }
            },
            new Product
            {
                Name = "Sesli Baby Paws Stitch Peluş B...",
                Description = "Sesli Baby Paws Stitch Peluş B..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/8ecf7_Sesli_Baby_Paws_Stitch_Pelus_BAW04000.jpg",
                Price = 1999.99M,
                Category = new List<string> { "Peluş Oyuncaklar" }
            },
            new Product
            {
                Name = "Sesli Baby Paws (Cocker Pembe Çanta)",
                Description = "Sesli Baby Paws (Cocker Pembe Çanta), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/0a0f0_Sesli_Baby_Paws_Yavru_Kopek_Pelus_BAW01000.jpg",
                Price = 1699.99M,
                Category = new List<string> { "Peluş Oyuncaklar" }
            },
            new Product
            {
                Name = "Sesli Baby Paws (Labrador Yeşil Çanta)",
                Description = "Sesli Baby Paws (Labrador Yeşil Çanta), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/2a518_Sesli_Baby_Paws_Yavru_Kopek_Pelus_BAW01000.jpg",
                Price = 1699.99M,
                Category = new List<string> { "Peluş Oyuncaklar" }
            },
            new Product
            {
                Name = "Rainbocorns Baby Eggzania Pelu...",
                Description = "Rainbocorns Baby Eggzania Pelu..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/328da_Rainbocorns_Baby_Eggzania_Pelus_Surpriz_Paket_S1_R.jpg",
                Price = 399.99M,
                Category = new List<string> { "Peluş Oyuncaklar" }
            },
            new Product
            {
                Name = "Sesli ve Hareketli Kulübeli Se...",
                Description = "Sesli ve Hareketli Kulübeli Se..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/01a8c_Sesli_ve_Hareketli_Kulubeli_Sevimli_Pelus_Kopek.jpg",
                Price = 749.00M,
                Category = new List<string> { "Peluş Oyuncaklar" }
            },
            new Product
            {
                Name = "Sesli ve Hareketli Kulübeli Se...",
                Description = "Sesli ve Hareketli Kulübeli Se..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/0596a_Sesli_ve_Hareketli_Kulubeli_Sevimli_Pelus_Tavsan.jpg",
                Price = 749.00M,
                Category = new List<string> { "Peluş Oyuncaklar" }
            },
            new Product
            {
                Name = "Coco Cones Tatl (Jelly)",
                Description = "Coco Cones Tatl (Jelly), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/0ece6_Coco_Cones_Tatli_Pelus_S1_CCN03000.jpg",
                Price = 429.99M,
                Category = new List<string> { "Peluş Oyuncaklar" }
            },
            new Product
            {
                Name = "Coco Cones Tatl (Mallow)",
                Description = "Coco Cones Tatl (Mallow), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/e102a_Coco_Cones_Tatli_Pelus_S1_CCN03000.jpg",
                Price = 429.99M,
                Category = new List<string> { "Peluş Oyuncaklar" }
            },
            new Product
            {
                Name = "Coco Cones Tatl (Chip)",
                Description = "Coco Cones Tatl (Chip), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/109f6_Coco_Cones_Tatli_Pelus_S1_CCN03000.jpg",
                Price = 429.99M,
                Category = new List<string> { "Peluş Oyuncaklar" }
            },
            new Product
            {
                Name = "Coco Cones Tatl (Lola)",
                Description = "Coco Cones Tatl (Lola), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/be319_Coco_Cones_Tatli_Pelus_S1_CCN03000.jpg",
                Price = 429.99M,
                Category = new List<string> { "Peluş Oyuncaklar" }
            },
            new Product
            {
                Name = "Coco Cones Tatl (Sally)",
                Description = "Coco Cones Tatl (Sally), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/b11f7_Coco_Cones_Tatli_Pelus_S1_CCN03000.jpg",
                Price = 429.99M,
                Category = new List<string> { "Peluş Oyuncaklar" }
            },
            new Product
            {
                Name = "Coco Cones Tatl (Perry)",
                Description = "Coco Cones Tatl (Perry), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/d82e5_Coco_Cones_Tatli_Pelus_S1_CCN03000.jpg",
                Price = 429.99M,
                Category = new List<string> { "Peluş Oyuncaklar" }
            },
            new Product
            {
                Name = "Coco Cones Pask (Bounce)",
                Description = "Coco Cones Pask (Bounce), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/0534f_Coco_Cones_Paskalya_Pelus_S1_CCN05000.jpg",
                Price = 429.99M,
                Category = new List<string> { "Peluş Oyuncaklar" }
            },
            new Product
            {
                Name = "Coco Cones Pask (Cornflake)",
                Description = "Coco Cones Pask (Cornflake), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/ffc3b_Coco_Cones_Paskalya_Pelus_S1_CCN05000.jpg",
                Price = 429.99M,
                Category = new List<string> { "Peluş Oyuncaklar" }
            },
            new Product
            {
                Name = "Coco Cones Pask (Poppy)",
                Description = "Coco Cones Pask (Poppy), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/85e92_Coco_Cones_Paskalya_Pelus_S1_CCN05000.jpg",
                Price = 429.99M,
                Category = new List<string> { "Peluş Oyuncaklar" }
            },
            new Product
            {
                Name = "Coco Cones Pask (Bop)",
                Description = "Coco Cones Pask (Bop), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/129f0_Coco_Cones_Paskalya_Pelus_S1_CCN05000.jpg",
                Price = 429.99M,
                Category = new List<string> { "Peluş Oyuncaklar" }
            },
            new Product
            {
                Name = "Coco Cones Pask (Mindy)",
                Description = "Coco Cones Pask (Mindy), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/2f481_Coco_Cones_Paskalya_Pelus_S1_CCN05000.jpg",
                Price = 429.99M,
                Category = new List<string> { "Peluş Oyuncaklar" }
            },
            new Product
            {
                Name = "Coco Cones Pask (Crumb)",
                Description = "Coco Cones Pask (Crumb), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/24e72_Coco_Cones_Paskalya_Pelus_S1_CCN05000.jpg",
                Price = 429.99M,
                Category = new List<string> { "Peluş Oyuncaklar" }
            },
            new Product
            {
                Name = "Coco Cones Sevg (Albert)",
                Description = "Coco Cones Sevg (Albert), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/b583c_Coco_Cones_Sevgili_Pelus_S1_CCN04000_.jpg",
                Price = 429.99M,
                Category = new List<string> { "Peluş Oyuncaklar" }
            },
            new Product
            {
                Name = "Coco Cones Sevg (Summer)",
                Description = "Coco Cones Sevg (Summer), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/a4bb4_Coco_Cones_Sevgili_Pelus_S1_CCN04000_.jpg",
                Price = 429.99M,
                Category = new List<string> { "Peluş Oyuncaklar" }
            },
            new Product
            {
                Name = "Coco Cones Sevg (Emma)",
                Description = "Coco Cones Sevg (Emma), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/a5acd_Coco_Cones_Sevgili_Pelus_S1_CCN04000_.jpg",
                Price = 429.99M,
                Category = new List<string> { "Peluş Oyuncaklar" }
            },
            new Product
            {
                Name = "Coco Cones Sevg (Tilly)",
                Description = "Coco Cones Sevg (Tilly), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/ce616_Coco_Cones_Sevgili_Pelus_S1_CCN04000_.jpg",
                Price = 429.99M,
                Category = new List<string> { "Peluş Oyuncaklar" }
            },
            new Product
            {
                Name = "Misfittens Gems (Mor Kapak)",
                Description = "Misfittens Gems (Mor Kapak), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/c8739_Misfittens_Gemstones_Surpriz_Pelus_S5.jpg",
                Price = 749.00M,
                Category = new List<string> { "Peluş Oyuncaklar" }
            },
            new Product
            {
                Name = "Misfittens Gems (Pembe Kapak)",
                Description = "Misfittens Gems (Pembe Kapak), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/bf22c_Misfittens_Gemstones_Surpriz_Pelus_S5.jpg",
                Price = 749.00M,
                Category = new List<string> { "Peluş Oyuncaklar" }
            },
            new Product
            {
                Name = "Misfittens Gems (Yeşil Kapak)",
                Description = "Misfittens Gems (Yeşil Kapak), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/70662_Misfittens_Gemstones_Surpriz_Pelus_S5.jpg",
                Price = 749.00M,
                Category = new List<string> { "Peluş Oyuncaklar" }
            },
            new Product
            {
                Name = "Misfittens Gems (Beyaz Kapak)",
                Description = "Misfittens Gems (Beyaz Kapak), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/2a031_Misfittens_Gemstones_Surpriz_Pelus_S5.jpg",
                Price = 749.00M,
                Category = new List<string> { "Peluş Oyuncaklar" }
            },
            new Product
            {
                Name = "Misfittens Gems (Mavi Kapak)",
                Description = "Misfittens Gems (Mavi Kapak), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/e54e2_Misfittens_Gemstones_Surpriz_Pelus_S5.jpg",
                Price = 749.00M,
                Category = new List<string> { "Peluş Oyuncaklar" }
            },
            new Product
            {
                Name = "1:64 Hot Wheels (Jby69 Fiat 500 Topolino (1936))",
                Description = "1:64 Hot Wheels (Jby69 Fiat 500 Topolino (1936)), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/8f4a1_164_Hot_Wheels_The_Hot_Ones_Die_Cast_Tekli_Arabala.jpg",
                Price = 249.99M,
                Category = new List<string> { "Oyuncak Arabalar" }
            },
            new Product
            {
                Name = "1:64 Hot Wheels (Jby71 Custom Otto)",
                Description = "1:64 Hot Wheels (Jby71 Custom Otto), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/ebf67_164_Hot_Wheels_The_Hot_Ones_Die_Cast_Tekli_Arabala.jpg",
                Price = 249.99M,
                Category = new List<string> { "Oyuncak Arabalar" }
            },
            new Product
            {
                Name = "1:64 Hot Wheels (Jby72 ’70 Toyota Celica)",
                Description = "1:64 Hot Wheels (Jby72 ’70 Toyota Celica), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/8bc42_164_Hot_Wheels_The_Hot_Ones_Die_Cast_Tekli_Arabala.jpg",
                Price = 249.99M,
                Category = new List<string> { "Oyuncak Arabalar" }
            },
            new Product
            {
                Name = "1:12 USB Şarjlı Uzaktan Kumand...",
                Description = "1:12 USB Şarjlı Uzaktan Kumand..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/29bf8_1_12_USB_Sarjli_Uzaktan_Kumandali_4x4_Arazi_Araci.jpg",
                Price = 3699.00M,
                Category = new List<string> { "Oyuncak Arabalar" }
            },
            new Product
            {
                Name = "1:24 Lamborghin (Yeşil)",
                Description = "1:24 Lamborghin (Yeşil), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/879a7_1_24_Lamborghini_Sian_FKP_37_Model_Araba.jpg",
                Price = 1199.00M,
                Category = new List<string> { "Oyuncak Arabalar" }
            },
            new Product
            {
                Name = "1:24 Lamborghin (Kırmızı)",
                Description = "1:24 Lamborghin (Kırmızı), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/6fc35_1_24_Lamborghini_Sian_FKP_37_Model_Araba.jpg",
                Price = 1199.00M,
                Category = new List<string> { "Oyuncak Arabalar" }
            },
            new Product
            {
                Name = "1:24 Lamborghini Revuelto Mode...",
                Description = "1:24 Lamborghini Revuelto Mode..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/8ac32_1_24_Lamborghini_Revuelto_Model_Araba.jpg",
                Price = 1199.00M,
                Category = new List<string> { "Oyuncak Arabalar" }
            },
            new Product
            {
                Name = "1:24 Lamborghin (Kırmızı)",
                Description = "1:24 Lamborghin (Kırmızı), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/2b5f3_1_24_Lamborghini_Countach_LPI_800-4_Model_Araba.jpg",
                Price = 1199.00M,
                Category = new List<string> { "Oyuncak Arabalar" }
            },
            new Product
            {
                Name = "1:24 Lamborghin (Beyaz)",
                Description = "1:24 Lamborghin (Beyaz), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/06e69_1_24_Lamborghini_Countach_LPI_800-4_Model_Araba.jpg",
                Price = 1199.00M,
                Category = new List<string> { "Oyuncak Arabalar" }
            },
            new Product
            {
                Name = "Sesli ve Işıklı Sürtmeli Mikse...",
                Description = "Sesli ve Işıklı Sürtmeli Mikse..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/de7c3_Sesli_ve_Isikli_Surtmeli_Mikser.jpg",
                Price = 269.99M,
                Category = new List<string> { "Oyuncak Arabalar" }
            },
            new Product
            {
                Name = "Sesli ve Işıklı Sürtmeli Kamyo...",
                Description = "Sesli ve Işıklı Sürtmeli Kamyo..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/9b62b_Sesli_ve_Isikli_Surtmeli_Kamyon.jpg",
                Price = 269.99M,
                Category = new List<string> { "Oyuncak Arabalar" }
            },
            new Product
            {
                Name = "Sesli ve Işıklı Sürtmeli Kepçe...",
                Description = "Sesli ve Işıklı Sürtmeli Kepçe..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/079bd_Sesli_ve_Isikli_Surtmeli_Kepce_.jpg",
                Price = 269.99M,
                Category = new List<string> { "Oyuncak Arabalar" }
            },
            new Product
            {
                Name = "1:24 Porsche 963 Model Araba",
                Description = "1:24 Porsche 963 Model Araba, Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/29c16_1_24_Porsche_963_Model_Araba.jpg",
                Price = 1249.00M,
                Category = new List<string> { "Oyuncak Arabalar" }
            },
            new Product
            {
                Name = "1:64 Hot Wheels (Jbl06 71 Amc Javelin Ve Amc Rebel Machine)",
                Description = "1:64 Hot Wheels (Jbl06 71 Amc Javelin Ve Amc Rebel Machine), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/23acb_164_Hot_Wheels_Premium_Car_Culture_2li_Paket_HBL96.jpg",
                Price = 699.99M,
                Category = new List<string> { "Oyuncak Arabalar" }
            },
            new Product
            {
                Name = "1:12 Nikko Hercules USB Şarjlı...",
                Description = "1:12 Nikko Hercules USB Şarjlı..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/1046c_1:12_Nikko_Hercules_USB_Sarjli_Uzaktan_Kumandali_A.jpg",
                Price = 2899.00M,
                Category = new List<string> { "Oyuncak Arabalar" }
            },
            new Product
            {
                Name = "Minibo Mini Mon (Dino Express Sarı Kasa)",
                Description = "Minibo Mini Mon (Dino Express Sarı Kasa), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/796fb_Minibo_Mini_Monster_Buyuk_Teker_Araba.jpg",
                Price = 149.99M,
                Category = new List<string> { "Oyuncak Arabalar" }
            },
            new Product
            {
                Name = "Minibo Mini Mon (Dino Express Yeşil Kasa)",
                Description = "Minibo Mini Mon (Dino Express Yeşil Kasa), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/60fc9_Minibo_Mini_Monster_Buyuk_Teker_Araba.jpg",
                Price = 149.99M,
                Category = new List<string> { "Oyuncak Arabalar" }
            },
            new Product
            {
                Name = "Minibo Mini Mon (Hot Dog Kırmızı Araba)",
                Description = "Minibo Mini Mon (Hot Dog Kırmızı Araba), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/97d1e_Minibo_Mini_Monster_Buyuk_Teker_Araba.jpg",
                Price = 149.99M,
                Category = new List<string> { "Oyuncak Arabalar" }
            },
            new Product
            {
                Name = "Minibo Mini Mon (Hot Dog Sarı Araba)",
                Description = "Minibo Mini Mon (Hot Dog Sarı Araba), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/207b1_Minibo_Mini_Monster_Buyuk_Teker_Araba.jpg",
                Price = 149.99M,
                Category = new List<string> { "Oyuncak Arabalar" }
            },
            new Product
            {
                Name = "Minibo Mini Mon (Off Road Turuncu Araba)",
                Description = "Minibo Mini Mon (Off Road Turuncu Araba), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/614eb_Minibo_Mini_Monster_Buyuk_Teker_Araba.jpg",
                Price = 149.99M,
                Category = new List<string> { "Oyuncak Arabalar" }
            },
            new Product
            {
                Name = "Minibo Mini Mon (Off Road Yeşil Araba)",
                Description = "Minibo Mini Mon (Off Road Yeşil Araba), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/df019_Minibo_Mini_Monster_Buyuk_Teker_Araba.jpg",
                Price = 149.99M,
                Category = new List<string> { "Oyuncak Arabalar" }
            },
            new Product
            {
                Name = "Spidey Ağ Fırla (Spin)",
                Description = "Spidey Ağ Fırla (Spin), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/540b1_Spidey_Ag_Firlatan_Araba_PDY21000.jpg",
                Price = 549.99M,
                Category = new List<string> { "Oyuncak Arabalar" }
            },
            new Product
            {
                Name = "Spidey Ağ Fırla (Ghost-Spider)",
                Description = "Spidey Ağ Fırla (Ghost-Spider), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/32351_Spidey_Ag_Firlatan_Araba_PDY21000.jpg",
                Price = 549.99M,
                Category = new List<string> { "Oyuncak Arabalar" }
            },
            new Product
            {
                Name = "Spidey Ağ Fırla (Spidey)",
                Description = "Spidey Ağ Fırla (Spidey), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/1334e_Spidey_Ag_Firlatan_Araba_PDY21000.jpg",
                Price = 549.99M,
                Category = new List<string> { "Oyuncak Arabalar" }
            },
            new Product
            {
                Name = "Spidey Araba PD (Spidey)",
                Description = "Spidey Araba PD (Spidey), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/bd8a0_Spidey_Araba_PDY20000.jpg",
                Price = 419.99M,
                Category = new List<string> { "Oyuncak Arabalar" }
            },
            new Product
            {
                Name = "Spidey Araba PD (Ghost-Spider)",
                Description = "Spidey Araba PD (Ghost-Spider), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/a49c3_Spidey_Araba_PDY20000.jpg",
                Price = 419.99M,
                Category = new List<string> { "Oyuncak Arabalar" }
            },
            new Product
            {
                Name = "Spidey Araba PD (Spin)",
                Description = "Spidey Araba PD (Spin), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/f6c74_Spidey_Araba_PDY20000.jpg",
                Price = 419.99M,
                Category = new List<string> { "Oyuncak Arabalar" }
            },
            new Product
            {
                Name = "Spidey Die Cast (Spidey)",
                Description = "Spidey Die Cast (Spidey), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/373b0_Spidey_Die_Cast_Araba_PDY16000.jpg",
                Price = 379.99M,
                Category = new List<string> { "Oyuncak Arabalar" }
            },
            new Product
            {
                Name = "Spidey Die Cast (Ghost-Spider)",
                Description = "Spidey Die Cast (Ghost-Spider), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/8977b_Spidey_Die_Cast_Araba_PDY16000.jpg",
                Price = 379.99M,
                Category = new List<string> { "Oyuncak Arabalar" }
            },
            new Product
            {
                Name = "Spidey Die Cast (Spin)",
                Description = "Spidey Die Cast (Spin), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/937ec_Spidey_Die_Cast_Araba_PDY16000.jpg",
                Price = 379.99M,
                Category = new List<string> { "Oyuncak Arabalar" }
            },
            new Product
            {
                Name = "Spidey Die Cast (Rhino)",
                Description = "Spidey Die Cast (Rhino), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/7803d_Spidey_Die_Cast_Araba_PDY16000.jpg",
                Price = 379.99M,
                Category = new List<string> { "Oyuncak Arabalar" }
            },
            new Product
            {
                Name = "Spidey Die Cast (Hulk)",
                Description = "Spidey Die Cast (Hulk), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/f9957_Spidey_Die_Cast_Araba_PDY16000.jpg",
                Price = 379.99M,
                Category = new List<string> { "Oyuncak Arabalar" }
            },
            new Product
            {
                Name = "Max Wheels 360 Derece Pist Set...",
                Description = "Max Wheels 360 Derece Pist Set..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/bce46_Max_Wheels_360_Derece_Pist_Seti_.jpg",
                Price = 399.99M,
                Category = new List<string> { "Oyuncak Arabalar" }
            },
            new Product
            {
                Name = "Crazoo Dino Transporter Pist S...",
                Description = "Crazoo Dino Transporter Pist S..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/c89bf_Crazoo_Dino_Transporter_Pist_Seti__.jpg",
                Price = 799.00M,
                Category = new List<string> { "Oyuncak Arabalar" }
            },
            new Product
            {
                Name = "Suncon Sesli ve Işıklı USB Şar...",
                Description = "Suncon Sesli ve Işıklı USB Şar..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/74484_Suncon_Sesli_ve_Isikli_USB_Sarjli_Uzaktan_Kumandal.jpg",
                Price = 2599.00M,
                Category = new List<string> { "Oyuncak Arabalar" }
            },
            new Product
            {
                Name = "Suncon Işıklı USB Şarjlı Uzakt...",
                Description = "Suncon Işıklı USB Şarjlı Uzakt..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/dfa40_Suncon_Isikli_USB_Sarjli_Uzaktan_Kumandali_Ekskava.jpg",
                Price = 1799.00M,
                Category = new List<string> { "Oyuncak Arabalar" }
            },
            new Product
            {
                Name = "1:24 Armored Batmobile Model A...",
                Description = "1:24 Armored Batmobile Model A..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/c15ea_1_24_Armored_Batmobile_Model_Araba_ve_Die_Cast_Fig.jpg",
                Price = 3999.00M,
                Category = new List<string> { "Oyuncak Arabalar" }
            },
            new Product
            {
                Name = "1:24 Fast & Furious 1989 F...",
                Description = "1:24 Fast & Furious 1989 F..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/45fe3_1:24_Fast_amp__Furious_1989_Ford_Mustang_GT_Die_Ca.jpg",
                Price = 1899.00M,
                Category = new List<string> { "Oyuncak Arabalar" }
            },
            new Product
            {
                Name = "1:24 Fast & Furious 1967 C...",
                Description = "1:24 Fast & Furious 1967 C..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/089e7_1:24_Fast_amp__Furious_1967_Chevrolet_El_Camino_Di.jpg",
                Price = 1899.00M,
                Category = new List<string> { "Oyuncak Arabalar" }
            },
            new Product
            {
                Name = "Disney Mickey Mouse Roadster U...",
                Description = "Disney Mickey Mouse Roadster U..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/85d5d_Disney_Mickey_Mouse_Roadster_Uzaktan_Kumandali_Ara.jpg",
                Price = 2299.00M,
                Category = new List<string> { "Oyuncak Arabalar" }
            },
            new Product
            {
                Name = "1:18 XT Racer U (Yeşil)",
                Description = "1:18 XT Racer U (Yeşil), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/bd988_118_XT_Racer_USB_Sarjli_Uzaktan_Kumandali_Araba.jpg",
                Price = 1459.00M,
                Category = new List<string> { "Oyuncak Arabalar" }
            },
            new Product
            {
                Name = "Işıklı Stunt Ru (Turuncu)",
                Description = "Işıklı Stunt Ru (Turuncu), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/18ae2_Isikli_Stunt_Runner_USB_Sarjli_Uzaktan_Kumandali_A.jpg",
                Price = 1099.00M,
                Category = new List<string> { "Oyuncak Arabalar" }
            },
            new Product
            {
                Name = "Işıklı Stunt Ru (Yeşil)",
                Description = "Işıklı Stunt Ru (Yeşil), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/303a2_Isikli_Stunt_Runner_USB_Sarjli_Uzaktan_Kumandali_A.jpg",
                Price = 1099.00M,
                Category = new List<string> { "Oyuncak Arabalar" }
            },
            new Product
            {
                Name = "Hot Wheels Monster Trucks Powe...",
                Description = "Hot Wheels Monster Trucks Powe..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/36b95_Hot_Wheels_Monster_Trucks_Power_Smashers_Multi_Pak.jpg",
                Price = 1799.99M,
                Category = new List<string> { "Oyuncak Arabalar" }
            },
            new Product
            {
                Name = "1:24 Monster Ja (Blue Thunder)",
                Description = "1:24 Monster Ja (Blue Thunder), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/5bf93_124_Monster_Jam_Die_Cast_Kamyon_S22.jpg",
                Price = 839.00M,
                Category = new List<string> { "Oyuncak Arabalar" }
            },
            new Product
            {
                Name = "1:24 Monster Ja (Dragonoid)",
                Description = "1:24 Monster Ja (Dragonoid), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/93ea3_124_Monster_Jam_Die_Cast_Kamyon_S22.jpg",
                Price = 839.00M,
                Category = new List<string> { "Oyuncak Arabalar" }
            },
            new Product
            {
                Name = "1:24 Monster Ja (El Toro Loco)",
                Description = "1:24 Monster Ja (El Toro Loco), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/518b4_124_Monster_Jam_Die_Cast_Kamyon_S22.jpg",
                Price = 839.00M,
                Category = new List<string> { "Oyuncak Arabalar" }
            },
            new Product
            {
                Name = "1:24 Monster Ja (Grave Digger)",
                Description = "1:24 Monster Ja (Grave Digger), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/edf30_124_Monster_Jam_Die_Cast_Kamyon_S22.jpg",
                Price = 839.00M,
                Category = new List<string> { "Oyuncak Arabalar" }
            },
            new Product
            {
                Name = "Puzzle City 64 Parça",
                Description = "Puzzle City 64 Parça, Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/fd228_Puzzle_City_64_Parca_.jpg",
                Price = 419.99M,
                Category = new List<string> { "Yapım Oyuncakları" }
            },
            new Product
            {
                Name = "LEGO F1 Koleksiyonluk Yarış Ar...",
                Description = "LEGO F1 Koleksiyonluk Yarış Ar..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/142d7_LEGO_F1_Koleksiyonluk_Yaris_Arabalari_71049_.jpg",
                Price = 159.00M,
                Category = new List<string> { "Yapım Oyuncakları" }
            },
            new Product
            {
                Name = "LEGO Disney Walt Disney Hatıra...",
                Description = "LEGO Disney Walt Disney Hatıra..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/ebcef_LEGO_Disney_Walt_Disney_Hatirasi_Kamera_43230_.jpg",
                Price = 3699.00M,
                Category = new List<string> { "Yapım Oyuncakları" }
            },
            new Product
            {
                Name = "LEGO Disney Pamuk Prenses ve Y...",
                Description = "LEGO Disney Pamuk Prenses ve Y..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/30694_LEGO_Disney_Pamuk_Prenses_ve_Yedi_Cucelerin_Evi_43.jpg",
                Price = 7999.00M,
                Category = new List<string> { "Yapım Oyuncakları" }
            },
            new Product
            {
                Name = "LEGO Marvel Iron Man’in Labora...",
                Description = "LEGO Marvel Iron Man’in Labora..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/25304_LEGO_Marvel_Iron_Manin_Laboratuvari_Zirh_Salonu_76.jpg",
                Price = 1899.00M,
                Category = new List<string> { "Yapım Oyuncakları" }
            },
            new Product
            {
                Name = "LEGO Marvel Hulk Kamyonu, Than...",
                Description = "LEGO Marvel Hulk Kamyonu, Than..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/e7305_LEGO_Marvel_Hulk_Kamyonu__Thanosa_Karsi_76312_.jpg",
                Price = 1049.00M,
                Category = new List<string> { "Yapım Oyuncakları" }
            },
            new Product
            {
                Name = "MAX Retro Sürpr (Kaset Çalar Radyo 157 Parça)",
                Description = "MAX Retro Sürpr (Kaset Çalar Radyo 157 Parça), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/f5622_MAX_Retro_Surprizi_MNB07000.jpg",
                Price = 549.99M,
                Category = new List<string> { "Yapım Oyuncakları" }
            },
            new Product
            {
                Name = "MAX Retro Sürpr (Polaroid Fotoğraf Makinesi 152 Parça)",
                Description = "MAX Retro Sürpr (Polaroid Fotoğraf Makinesi 152 Parça), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/07504_MAX_Retro_Surprizi_MNB07000.jpg",
                Price = 549.99M,
                Category = new List<string> { "Yapım Oyuncakları" }
            },
            new Product
            {
                Name = "MAX Retro Sürpr (Atari Oyun Makinesi 162 Parça)",
                Description = "MAX Retro Sürpr (Atari Oyun Makinesi 162 Parça), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/03137_MAX_Retro_Surprizi_MNB07000.jpg",
                Price = 549.99M,
                Category = new List<string> { "Yapım Oyuncakları" }
            },
            new Product
            {
                Name = "MAX Retro Sürpr (Çevirmeli Telefon 141 Parça)",
                Description = "MAX Retro Sürpr (Çevirmeli Telefon 141 Parça), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/a295a_MAX_Retro_Surprizi_MNB07000.jpg",
                Price = 549.99M,
                Category = new List<string> { "Yapım Oyuncakları" }
            },
            new Product
            {
                Name = "MAX Retro Sürpr (Bilgisayar 221 Parça)",
                Description = "MAX Retro Sürpr (Bilgisayar 221 Parça), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/5dde7_MAX_Retro_Surprizi_MNB07000.jpg",
                Price = 549.99M,
                Category = new List<string> { "Yapım Oyuncakları" }
            },
            new Product
            {
                Name = "MAX Retro Sürpr (Televizyon 168 Parça)",
                Description = "MAX Retro Sürpr (Televizyon 168 Parça), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/6e58d_MAX_Retro_Surprizi_MNB07000.jpg",
                Price = 549.99M,
                Category = new List<string> { "Yapım Oyuncakları" }
            },
            new Product
            {
                Name = "MAX Uzun Çiçekl (Zambak Ve Kokulu Sümbül 232 Parça)",
                Description = "MAX Uzun Çiçekl (Zambak Ve Kokulu Sümbül 232 Parça), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/38850_MAX_Uzun_Cicekler_Surprizi_MNB05000.jpg",
                Price = 549.99M,
                Category = new List<string> { "Yapım Oyuncakları" }
            },
            new Product
            {
                Name = "MAX Uzun Çiçekl (Yalancı Tarak Otu Ve İris 237 Parça)",
                Description = "MAX Uzun Çiçekl (Yalancı Tarak Otu Ve İris 237 Parça), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/327db_MAX_Uzun_Cicekler_Surprizi_MNB05000.jpg",
                Price = 549.99M,
                Category = new List<string> { "Yapım Oyuncakları" }
            },
            new Product
            {
                Name = "MAX Uzun Çiçekl (Balon Çiçeği Ve Ayçiçeği 212 Parça)",
                Description = "MAX Uzun Çiçekl (Balon Çiçeği Ve Ayçiçeği 212 Parça), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/b39d4_MAX_Uzun_Cicekler_Surprizi_MNB05000.jpg",
                Price = 549.99M,
                Category = new List<string> { "Yapım Oyuncakları" }
            },
            new Product
            {
                Name = "MAX Uzun Çiçekl (Papatya Ve Lavanta 243 Parça)",
                Description = "MAX Uzun Çiçekl (Papatya Ve Lavanta 243 Parça), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/36dd1_MAX_Uzun_Cicekler_Surprizi_MNB05000.jpg",
                Price = 549.99M,
                Category = new List<string> { "Yapım Oyuncakları" }
            },
            new Product
            {
                Name = "MAX Uzun Çiçekl (Gül Ve Vadideki Zambak 204)",
                Description = "MAX Uzun Çiçekl (Gül Ve Vadideki Zambak 204), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/79153_MAX_Uzun_Cicekler_Surprizi_MNB05000.jpg",
                Price = 549.99M,
                Category = new List<string> { "Yapım Oyuncakları" }
            },
            new Product
            {
                Name = "MAX Saksı Bitki (Ortanca 233 Parça)",
                Description = "MAX Saksı Bitki (Ortanca 233 Parça), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/c4d60_MAX_Saksi_Bitkileri_Surprizi_MNB08000.jpg",
                Price = 549.99M,
                Category = new List<string> { "Yapım Oyuncakları" }
            },
            new Product
            {
                Name = "MAX Saksı Bitki (Ayçiçeği 151 Parça)",
                Description = "MAX Saksı Bitki (Ayçiçeği 151 Parça), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/04fc1_MAX_Saksi_Bitkileri_Surprizi_MNB08000.jpg",
                Price = 549.99M,
                Category = new List<string> { "Yapım Oyuncakları" }
            },
            new Product
            {
                Name = "MAX Saksı Bitki (Gelin Çiçeği 186 Parça)",
                Description = "MAX Saksı Bitki (Gelin Çiçeği 186 Parça), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/78b33_MAX_Saksi_Bitkileri_Surprizi_MNB08000.jpg",
                Price = 549.99M,
                Category = new List<string> { "Yapım Oyuncakları" }
            },
            new Product
            {
                Name = "MAX Saksı Bitki (Çan Çiçeği 192 Parça)",
                Description = "MAX Saksı Bitki (Çan Çiçeği 192 Parça), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/4fddd_MAX_Saksi_Bitkileri_Surprizi_MNB08000.jpg",
                Price = 549.99M,
                Category = new List<string> { "Yapım Oyuncakları" }
            },
            new Product
            {
                Name = "MAX Saksı Bitki (Kozmos 152 Parça)",
                Description = "MAX Saksı Bitki (Kozmos 152 Parça), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/45a0a_MAX_Saksi_Bitkileri_Surprizi_MNB08000.jpg",
                Price = 549.99M,
                Category = new List<string> { "Yapım Oyuncakları" }
            },
            new Product
            {
                Name = "MAX Saksı Bitki (Ay Orkidesi 169 Parça)",
                Description = "MAX Saksı Bitki (Ay Orkidesi 169 Parça), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/f4046_MAX_Saksi_Bitkileri_Surprizi_MNB08000.jpg",
                Price = 549.99M,
                Category = new List<string> { "Yapım Oyuncakları" }
            },
            new Product
            {
                Name = "MAX Gül Sürprizi 5UC00000",
                Description = "MAX Gül Sürprizi 5UC00000, Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/8afb6_MAX_Gul_Surprizi_5UC00000.jpg",
                Price = 549.99M,
                Category = new List<string> { "Yapım Oyuncakları" }
            },
            new Product
            {
                Name = "LEGO Speed Champions Williams ...",
                Description = "LEGO Speed Champions Williams ..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/95e1c_LEGO_Speed_Champions_Williams_Racing_FW46_F1_Yaris.jpg",
                Price = 899.00M,
                Category = new List<string> { "Yapım Oyuncakları" }
            },
            new Product
            {
                Name = "LEGO Speed Champions McLaren F...",
                Description = "LEGO Speed Champions McLaren F..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/464e1_LEGO_Speed_Champions_McLaren_F1_Team_MCL38_Yaris_A.jpg",
                Price = 899.00M,
                Category = new List<string> { "Yapım Oyuncakları" }
            },
            new Product
            {
                Name = "LEGO Speed Champions MoneyGram...",
                Description = "LEGO Speed Champions MoneyGram..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/35844_LEGO_Speed_Champions_MoneyGram_Haas_F1_Team_VF_24_.jpg",
                Price = 899.00M,
                Category = new List<string> { "Yapım Oyuncakları" }
            },
            new Product
            {
                Name = "LEGO Speed Champions KICK Saub...",
                Description = "LEGO Speed Champions KICK Saub..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/c142c_LEGO_Speed_Champions_KICK_Sauber_F1_Team_C44_Yaris.jpg",
                Price = 949.00M,
                Category = new List<string> { "Yapım Oyuncakları" }
            },
            new Product
            {
                Name = "LEGO Speed Champions Visa Cash...",
                Description = "LEGO Speed Champions Visa Cash..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/d009d_LEGO_Speed_Champions_Visa_Cash_App_RB_VCARB_01_F1_.jpg",
                Price = 949.00M,
                Category = new List<string> { "Yapım Oyuncakları" }
            },
            new Product
            {
                Name = "LEGO Speed Champions Aston Mar...",
                Description = "LEGO Speed Champions Aston Mar..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/71f70_LEGO_Speed_Champions_Aston_Martin_Aramco_F1_AMR24_.jpg",
                Price = 899.00M,
                Category = new List<string> { "Yapım Oyuncakları" }
            },
            new Product
            {
                Name = "LEGO Speed Champions Mercedes-...",
                Description = "LEGO Speed Champions Mercedes-..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/42fe3_LEGO_Speed_Champions_Mercedes_AMG_F1_W15_Yaris_Ara.jpg",
                Price = 899.00M,
                Category = new List<string> { "Yapım Oyuncakları" }
            },
            new Product
            {
                Name = "LEGO Speed Champions Oracle Re...",
                Description = "LEGO Speed Champions Oracle Re..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/dec75_LEGO_Speed_Champions_Oracle_Red_Bull_Racing_RB20_F.jpg",
                Price = 899.00M,
                Category = new List<string> { "Yapım Oyuncakları" }
            },
            new Product
            {
                Name = "LEGO Speed Champions Ferrari S...",
                Description = "LEGO Speed Champions Ferrari S..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/599be_LEGO_Speed_Champions_Ferrari_SF24_F1_Yaris_Arabasi.jpg",
                Price = 899.00M,
                Category = new List<string> { "Yapım Oyuncakları" }
            },
            new Product
            {
                Name = "LEGO Horizon Adventures Aloy v...",
                Description = "LEGO Horizon Adventures Aloy v..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/c4622_LEGO_Horizon_Adventures_Aloy_ve_Varl_Metalkabuk_ve.jpg",
                Price = 1799.00M,
                Category = new List<string> { "Yapım Oyuncakları" }
            },
            new Product
            {
                Name = "LEGO Marvel X-Men: X-Mansion 7...",
                Description = "LEGO Marvel X-Men: X-Mansion 7..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/2abd5_LEGO_Marvel_XMen_X-Mansion_76294_.jpg",
                Price = 12999.00M,
                Category = new List<string> { "Yapım Oyuncakları" }
            },
            new Product
            {
                Name = "LEGO Disney Moana 2 Heihei 432...",
                Description = "LEGO Disney Moana 2 Heihei 432..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/77071_LEGO_Disney_Moana_2_Heihei_43272_.jpg",
                Price = 1399.00M,
                Category = new List<string> { "Yapım Oyuncakları" }
            },
            new Product
            {
                Name = "LEGO Disney Princess Sindirell...",
                Description = "LEGO Disney Princess Sindirell..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/145fd_LEGO_Disney_Princess_Sindirellanin_Elbisesi_43266_.jpg",
                Price = 1399.00M,
                Category = new List<string> { "Yapım Oyuncakları" }
            },
            new Product
            {
                Name = "LEGO Disney Kötüleri Malefiz'i...",
                Description = "LEGO Disney Kötüleri Malefiz'i..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/76029_LEGO_Disney_Kotuleri_Malefizin_ve_Cruella_De_Vilin.jpg",
                Price = 2599.00M,
                Category = new List<string> { "Yapım Oyuncakları" }
            },
            new Product
            {
                Name = "LEGO Technic Ferrari SF-24 F1 ...",
                Description = "LEGO Technic Ferrari SF-24 F1 ..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/b1b4a_LEGO_Technic_Ferrari_SF_24_F1_Araba_42207_.jpg",
                Price = 9399.00M,
                Category = new List<string> { "Yapım Oyuncakları" }
            },
            new Product
            {
                Name = "LEGO Technic Chevrolet Corvett...",
                Description = "LEGO Technic Chevrolet Corvett..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/b3bdd_LEGO_Technic_Chevrolet_Corvette_Stingray_42205.jpg",
                Price = 2099.00M,
                Category = new List<string> { "Yapım Oyuncakları" }
            },
            new Product
            {
                Name = "LEGO Technic Fast and Furious ...",
                Description = "LEGO Technic Fast and Furious ..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/b07aa_LEGO_Technic_Fast_and_Furious_Toyota_Supra_MK4_422.jpg",
                Price = 2199.00M,
                Category = new List<string> { "Yapım Oyuncakları" }
            },
            new Product
            {
                Name = "LEGO Technic Damperli Kamyon 4...",
                Description = "LEGO Technic Damperli Kamyon 4..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/2e566_LEGO_Technic_Damperli_Kamyon_42203_.jpg",
                Price = 1799.00M,
                Category = new List<string> { "Yapım Oyuncakları" }
            },
            new Product
            {
                Name = "LEGO Creator 3’ü 1 Arada Vahşi...",
                Description = "LEGO Creator 3’ü 1 Arada Vahşi..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/72624_LEGO_Creator_3u_1_Arada_Vahsi_Hayvanlar_Pembe_Flam.jpg",
                Price = 999.00M,
                Category = new List<string> { "Yapım Oyuncakları" }
            },
            new Product
            {
                Name = "LEGO Architecture Trevi Çeşmes...",
                Description = "LEGO Architecture Trevi Çeşmes..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/08780_LEGO_Architecture_Trevi_Cesmesi_21062_.jpg",
                Price = 5999.00M,
                Category = new List<string> { "Yapım Oyuncakları" }
            },
            new Product
            {
                Name = "LEGO Classic Sihirli Saydam Ku...",
                Description = "LEGO Classic Sihirli Saydam Ku..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/df01b_LEGO_Classic_Sihirli_Saydam_Kutu_11040_.jpg",
                Price = 779.00M,
                Category = new List<string> { "Yapım Oyuncakları" }
            },
            new Product
            {
                Name = "LEGO DUPLO Disney Ayı Winnie’n...",
                Description = "LEGO DUPLO Disney Ayı Winnie’n..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/98996_LEGO_DUPLO_Disney_Ayi_Winnie___nin_Dogum_Gunu_Part.jpg",
                Price = 749.00M,
                Category = new List<string> { "Yapım Oyuncakları" }
            },
            new Product
            {
                Name = "LEGO DUPLO Disney Mickey Fare’...",
                Description = "LEGO DUPLO Disney Mickey Fare’..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/e542e_LEGO_DUPLO_Disney_Mickey_Farenin_Kulup_Evi_ve_Arab.jpg",
                Price = 749.00M,
                Category = new List<string> { "Yapım Oyuncakları" }
            },
            new Product
            {
                Name = "LEGO DUPLO Peppa Pig Lunapark ...",
                Description = "LEGO DUPLO Peppa Pig Lunapark ..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/25f75_LEGO_DUPLO_Peppa_Pig_Lunapark_10453.jpg",
                Price = 1499.00M,
                Category = new List<string> { "Yapım Oyuncakları" }
            },
            new Product
            {
                Name = "Barbie Dream Besties Paten Par...",
                Description = "Barbie Dream Besties Paten Par..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/59611_Barbie_Dream_Besties_Paten_Partisi_Teresa_Bebek_ve.jpg",
                Price = 1499.99M,
                Category = new List<string> { "Oyuncak Bebekler ve Aksesuarları" }
            },
            new Product
            {
                Name = "Barbie Dream Besties Paten Par...",
                Description = "Barbie Dream Besties Paten Par..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/8b7a5_Barbie_Dream_Besties_Paten_Partisi_Brooklyn_Bebek_.jpg",
                Price = 1499.99M,
                Category = new List<string> { "Oyuncak Bebekler ve Aksesuarları" }
            },
            new Product
            {
                Name = "Barbie Dream Besties Paten Par...",
                Description = "Barbie Dream Besties Paten Par..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/5a855_Barbie_Dream_Besties_Paten_Partisi_Malibu_Bebek_ve.jpg",
                Price = 1499.99M,
                Category = new List<string> { "Oyuncak Bebekler ve Aksesuarları" }
            },
            new Product
            {
                Name = "Cicciobello Bahçıvan Bebek 30 ...",
                Description = "Cicciobello Bahçıvan Bebek 30 ..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/37131_Cicciobello_Bahcivan_Bebek_30_cm_CCBJ2000.jpg",
                Price = 1499.99M,
                Category = new List<string> { "Oyuncak Bebekler ve Aksesuarları" }
            },
            new Product
            {
                Name = "Barbie Astronot 60. Yıl Dönümü...",
                Description = "Barbie Astronot 60. Yıl Dönümü..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/a725e_Barbie_Astronot_60_Yil_Donumu_Nostaljik_Bebek_JBJ4.jpg",
                Price = 2299.99M,
                Category = new List<string> { "Oyuncak Bebekler ve Aksesuarları" }
            },
            new Product
            {
                Name = "Decora Girlz Mo (Electra)",
                Description = "Decora Girlz Mo (Electra), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/fd76f_Decora_Girlz_Moda_Bebegi.jpg",
                Price = 899.99M,
                Category = new List<string> { "Oyuncak Bebekler ve Aksesuarları" }
            },
            new Product
            {
                Name = "Decora Girlz Mo (Sprinkles)",
                Description = "Decora Girlz Mo (Sprinkles), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/8d970_Decora_Girlz_Moda_Bebegi.jpg",
                Price = 899.99M,
                Category = new List<string> { "Oyuncak Bebekler ve Aksesuarları" }
            },
            new Product
            {
                Name = "Cry Babies Love Banyo Zamanı D...",
                Description = "Cry Babies Love Banyo Zamanı D..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/55349_Cry_Babies_Love_Banyo_Zamani_Dana_Bebek_30_cm_CYB6.jpg",
                Price = 1799.99M,
                Category = new List<string> { "Oyuncak Bebekler ve Aksesuarları" }
            },
            new Product
            {
                Name = "Cry Babies Love Banyo Zamanı Z...",
                Description = "Cry Babies Love Banyo Zamanı Z..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/19737_Cry_Babies_Love_Banyo_Zamani_Zoe_Bebek_30_cm_CYB67.jpg",
                Price = 1749.99M,
                Category = new List<string> { "Oyuncak Bebekler ve Aksesuarları" }
            },
            new Product
            {
                Name = "Cry Babies Ağla (Açık Pembe Pijamalı Jenna)",
                Description = "Cry Babies Ağla (Açık Pembe Pijamalı Jenna), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/a4b55_Cry_Babies_Aglayan_Yumus_Newborn_Bebek_CYB68000.jpg",
                Price = 999.99M,
                Category = new List<string> { "Oyuncak Bebekler ve Aksesuarları" }
            },
            new Product
            {
                Name = "Cry Babies Ağla (Kırmızı Pijamalı Lady)",
                Description = "Cry Babies Ağla (Kırmızı Pijamalı Lady), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/814a4_Cry_Babies_Aglayan_Yumus_Newborn_Bebek_CYB68000.jpg",
                Price = 999.99M,
                Category = new List<string> { "Oyuncak Bebekler ve Aksesuarları" }
            },
            new Product
            {
                Name = "Cry Babies Ağla (Mavi Pijamalı Roxy)",
                Description = "Cry Babies Ağla (Mavi Pijamalı Roxy), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/3a1ca_Cry_Babies_Aglayan_Yumus_Newborn_Bebek_CYB68000.jpg",
                Price = 999.99M,
                Category = new List<string> { "Oyuncak Bebekler ve Aksesuarları" }
            },
            new Product
            {
                Name = "Cry Babies Ağla (Mor Pijamalı Lexi)",
                Description = "Cry Babies Ağla (Mor Pijamalı Lexi), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/24231_Cry_Babies_Aglayan_Yumus_Newborn_Bebek_CYB68000.jpg",
                Price = 999.99M,
                Category = new List<string> { "Oyuncak Bebekler ve Aksesuarları" }
            },
            new Product
            {
                Name = "Cry Babies Ağla (Yeşil Pijamalı Lara)",
                Description = "Cry Babies Ağla (Yeşil Pijamalı Lara), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/299dd_Cry_Babies_Aglayan_Yumus_Newborn_Bebek_CYB68000.jpg",
                Price = 999.99M,
                Category = new List<string> { "Oyuncak Bebekler ve Aksesuarları" }
            },
            new Product
            {
                Name = "Cry Babies MT D (Daisy)",
                Description = "Cry Babies MT D (Daisy), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/0fbcb_Cry_Babies_MT_Disney_Bebegi_CYM18000.jpg",
                Price = 699.99M,
                Category = new List<string> { "Oyuncak Bebekler ve Aksesuarları" }
            },
            new Product
            {
                Name = "Cry Babies MT D (Marie)",
                Description = "Cry Babies MT D (Marie), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/5e3d5_Cry_Babies_MT_Disney_Bebegi_CYM18000.jpg",
                Price = 699.99M,
                Category = new List<string> { "Oyuncak Bebekler ve Aksesuarları" }
            },
            new Product
            {
                Name = "Cry Babies MT D (Mickey)",
                Description = "Cry Babies MT D (Mickey), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/9331f_Cry_Babies_MT_Disney_Bebegi_CYM18000.jpg",
                Price = 699.99M,
                Category = new List<string> { "Oyuncak Bebekler ve Aksesuarları" }
            },
            new Product
            {
                Name = "Cry Babies MT D (Minnie)",
                Description = "Cry Babies MT D (Minnie), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/405cc_Cry_Babies_MT_Disney_Bebegi_CYM18000.jpg",
                Price = 699.99M,
                Category = new List<string> { "Oyuncak Bebekler ve Aksesuarları" }
            },
            new Product
            {
                Name = "Cry Babies MT D (Stitch)",
                Description = "Cry Babies MT D (Stitch), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/eccaf_Cry_Babies_MT_Disney_Bebegi_CYM18000.jpg",
                Price = 699.99M,
                Category = new List<string> { "Oyuncak Bebekler ve Aksesuarları" }
            },
            new Product
            {
                Name = "Cry Babies MT D (Winnie The Pooh)",
                Description = "Cry Babies MT D (Winnie The Pooh), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/8010a_Cry_Babies_MT_Disney_Bebegi_CYM18000.jpg",
                Price = 699.99M,
                Category = new List<string> { "Oyuncak Bebekler ve Aksesuarları" }
            },
            new Product
            {
                Name = "Cry Babies MT G (Coney)",
                Description = "Cry Babies MT G (Coney), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/86d14_Cry_Babies_MT_Gold_Serisi_Bebegi_CYM19000.jpg",
                Price = 499.99M,
                Category = new List<string> { "Oyuncak Bebekler ve Aksesuarları" }
            },
            new Product
            {
                Name = "Cry Babies MT G (Dreamy)",
                Description = "Cry Babies MT G (Dreamy), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/65008_Cry_Babies_MT_Gold_Serisi_Bebegi_CYM19000.jpg",
                Price = 499.99M,
                Category = new List<string> { "Oyuncak Bebekler ve Aksesuarları" }
            },
            new Product
            {
                Name = "Cry Babies MT G (Lady)",
                Description = "Cry Babies MT G (Lady), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/30c02_Cry_Babies_MT_Gold_Serisi_Bebegi_CYM19000.jpg",
                Price = 499.99M,
                Category = new List<string> { "Oyuncak Bebekler ve Aksesuarları" }
            },
            new Product
            {
                Name = "Cry Babies MT G (Loretta)",
                Description = "Cry Babies MT G (Loretta), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/02bfa_Cry_Babies_MT_Gold_Serisi_Bebegi_CYM19000.jpg",
                Price = 499.99M,
                Category = new List<string> { "Oyuncak Bebekler ve Aksesuarları" }
            },
            new Product
            {
                Name = "Cry Babies MT G (Narvie)",
                Description = "Cry Babies MT G (Narvie), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/a6365_Cry_Babies_MT_Gold_Serisi_Bebegi_CYM19000.jpg",
                Price = 499.99M,
                Category = new List<string> { "Oyuncak Bebekler ve Aksesuarları" }
            },
            new Product
            {
                Name = "Sesli Disney Karlar Ülkesi Şar...",
                Description = "Sesli Disney Karlar Ülkesi Şar..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/abcc5_Sesli_Disney_Karlar_Ulkesi_Sarki_Soyleyen_Elsa_Beb.jpg",
                Price = 1399.99M,
                Category = new List<string> { "Oyuncak Bebekler ve Aksesuarları" }
            },
            new Product
            {
                Name = "Işıklı Dreameez Denizkızı",
                Description = "Işıklı Dreameez Denizkızı, Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/774e6_Isikli_Dreameez_Denizkizi_.jpg",
                Price = 899.00M,
                Category = new List<string> { "Oyuncak Bebekler ve Aksesuarları" }
            },
            new Product
            {
                Name = "Dreameez Prense (Mor Elbiseli)",
                Description = "Dreameez Prense (Mor Elbiseli), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/3920d_Dreameez_Prenses_Bebek_.jpg",
                Price = 449.99M,
                Category = new List<string> { "Oyuncak Bebekler ve Aksesuarları" }
            },
            new Product
            {
                Name = "Dreameez Prense (Pembe Elbiseli)",
                Description = "Dreameez Prense (Pembe Elbiseli), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/627fa_Dreameez_Prenses_Bebek_.jpg",
                Price = 449.99M,
                Category = new List<string> { "Oyuncak Bebekler ve Aksesuarları" }
            },
            new Product
            {
                Name = "Dreameez Prense (Unicorn Taçlı)",
                Description = "Dreameez Prense (Unicorn Taçlı), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/4d9c4_Dreameez_Prenses_Bebek_.jpg",
                Price = 449.99M,
                Category = new List<string> { "Oyuncak Bebekler ve Aksesuarları" }
            },
            new Product
            {
                Name = "Dreameez Prense (Kahverengi Saçlı)",
                Description = "Dreameez Prense (Kahverengi Saçlı), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/f4032_Dreameez_Prenses_Bebek_.jpg",
                Price = 449.99M,
                Category = new List<string> { "Oyuncak Bebekler ve Aksesuarları" }
            },
            new Product
            {
                Name = "Dreameez Fantasty 5'li Bebek S...",
                Description = "Dreameez Fantasty 5'li Bebek S..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/7b677_Dreameez_Fantasty_5li_Bebek_Seti.jpg",
                Price = 1699.00M,
                Category = new List<string> { "Oyuncak Bebekler ve Aksesuarları" }
            },
            new Product
            {
                Name = "Dreameez Mini P (Beyaz Saçlı)",
                Description = "Dreameez Mini P (Beyaz Saçlı), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/8fd31_Dreameez_Mini_Prenses_Bebek_.jpg",
                Price = 179.99M,
                Category = new List<string> { "Oyuncak Bebekler ve Aksesuarları" }
            },
            new Product
            {
                Name = "Dreameez Mini P (Kahverengi Saçlı)",
                Description = "Dreameez Mini P (Kahverengi Saçlı), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/020a3_Dreameez_Mini_Prenses_Bebek_.jpg",
                Price = 179.99M,
                Category = new List<string> { "Oyuncak Bebekler ve Aksesuarları" }
            },
            new Product
            {
                Name = "Dreameez Mini P (Mor Saçlı)",
                Description = "Dreameez Mini P (Mor Saçlı), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/f16f5_Dreameez_Mini_Prenses_Bebek_.jpg",
                Price = 179.99M,
                Category = new List<string> { "Oyuncak Bebekler ve Aksesuarları" }
            },
            new Product
            {
                Name = "Dreameez Mini P (Sarı Saçlı)",
                Description = "Dreameez Mini P (Sarı Saçlı), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/2db7c_Dreameez_Mini_Prenses_Bebek_.jpg",
                Price = 179.99M,
                Category = new List<string> { "Oyuncak Bebekler ve Aksesuarları" }
            },
            new Product
            {
                Name = "Disney Karlar Ülkesi Eğlenceli...",
                Description = "Disney Karlar Ülkesi Eğlenceli..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/cf01c_Disney_Karlar_Ulkesi_Eglenceli_Surprizler_Elsa_Beb.jpg",
                Price = 1299.99M,
                Category = new List<string> { "Oyuncak Bebekler ve Aksesuarları" }
            },
            new Product
            {
                Name = "Disney Karlar Ülkesi Buz Kales...",
                Description = "Disney Karlar Ülkesi Buz Kales..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/1fd44_Disney_Karlar_Ulkesi_Buz_Kalesi_Surpriz_Paket_JCR9.jpg",
                Price = 699.99M,
                Category = new List<string> { "Oyuncak Bebekler ve Aksesuarları" }
            },
            new Product
            {
                Name = "Mini BarbieLand (Evcil Hayvan Butiği Jcr30)",
                Description = "Mini BarbieLand (Evcil Hayvan Butiği Jcr30), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/73fc0_Mini_BarbieLand_Bebek_ve_Oyun_Seti_JCR29_.jpg",
                Price = 399.99M,
                Category = new List<string> { "Oyuncak Bebekler ve Aksesuarları" }
            },
            new Product
            {
                Name = "Mini BarbieLand (Fırın Jcr31)",
                Description = "Mini BarbieLand (Fırın Jcr31), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/03e57_Mini_BarbieLand_Bebek_ve_Oyun_Seti_JCR29_.jpg",
                Price = 399.99M,
                Category = new List<string> { "Oyuncak Bebekler ve Aksesuarları" }
            },
            new Product
            {
                Name = "Mini BarbieLand (Kuaför Salonu Jcr32)",
                Description = "Mini BarbieLand (Kuaför Salonu Jcr32), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/218ac_Mini_BarbieLand_Bebek_ve_Oyun_Seti_JCR29_.jpg",
                Price = 399.99M,
                Category = new List<string> { "Oyuncak Bebekler ve Aksesuarları" }
            },
            new Product
            {
                Name = "Mini BarbieLand (Süpermarket Jcr33)",
                Description = "Mini BarbieLand (Süpermarket Jcr33), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/b8be0_Mini_BarbieLand_Bebek_ve_Oyun_Seti_JCR29_.jpg",
                Price = 399.99M,
                Category = new List<string> { "Oyuncak Bebekler ve Aksesuarları" }
            },
            new Product
            {
                Name = "Mini BarbieLand Color Reveal B...",
                Description = "Mini BarbieLand Color Reveal B..., Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/c4e8e_Mini_BarbieLand_Color_Reveal_Bebek_Surpriz_Paket_J.jpg",
                Price = 199.99M,
                Category = new List<string> { "Oyuncak Bebekler ve Aksesuarları" }
            },
            new Product
            {
                Name = "Barbie Parti Pa (Mor Kutu Jfg70)",
                Description = "Barbie Parti Pa (Mor Kutu Jfg70), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/3736d_Barbie_Parti_Paketi_Minik_Dostumun_Dogum_Gunu_Seri.jpg",
                Price = 1099.99M,
                Category = new List<string> { "Oyuncak Bebekler ve Aksesuarları" }
            },
            new Product
            {
                Name = "Barbie Parti Pa (Mavi Kutu Jfg71)",
                Description = "Barbie Parti Pa (Mavi Kutu Jfg71), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/88959_Barbie_Parti_Paketi_Minik_Dostumun_Dogum_Gunu_Seri.jpg",
                Price = 1099.99M,
                Category = new List<string> { "Oyuncak Bebekler ve Aksesuarları" }
            },
            new Product
            {
                Name = "Barbie Parti Pa (Pembe Kutu Jfg72)",
                Description = "Barbie Parti Pa (Pembe Kutu Jfg72), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/7992c_Barbie_Parti_Paketi_Minik_Dostumun_Dogum_Gunu_Seri.jpg",
                Price = 1099.99M,
                Category = new List<string> { "Oyuncak Bebekler ve Aksesuarları" }
            },
            new Product
            {
                Name = "Sesli Bebelou T (Pembe)",
                Description = "Sesli Bebelou T (Pembe), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/97eb7_Sesli_Bebelou_Tuvalet_Zamani_Bebek_Seti_32_cm.jpg",
                Price = 749.00M,
                Category = new List<string> { "Oyuncak Bebekler ve Aksesuarları" }
            },
            new Product
            {
                Name = "Sesli Bebelou T (Mavi)",
                Description = "Sesli Bebelou T (Mavi), Toyzzshop koleksiyonunun bir parçasıdır.",
                ImageFile = "https://docsd.toyzzshop.com/product/300x300/8a116_Sesli_Bebelou_Tuvalet_Zamani_Bebek_Seti_32_cm.jpg",
                Price = 749.00M,
                Category = new List<string> { "Oyuncak Bebekler ve Aksesuarları" }
            },
        };

    }
}
