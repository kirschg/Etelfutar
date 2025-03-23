-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Gép: 127.0.0.1
-- Létrehozás ideje: 2025. Már 23. 18:25
-- Kiszolgáló verziója: 10.4.32-MariaDB
-- PHP verzió: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Adatbázis: `etelfutar`
--

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `chain`
--

CREATE TABLE `chain` (
  `Id` int(11) NOT NULL,
  `Nev` varchar(64) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- A tábla adatainak kiíratása `chain`
--

INSERT INTO `chain` (`Id`, `Nev`) VALUES
(7, 'Alcatraz Ételbár'),
(0, 'Burger King'),
(8, 'Chens Étterem'),
(6, 'Corvinus Étterem'),
(2, 'KFC'),
(5, 'Lángos Ház'),
(3, 'Mcdonalds'),
(4, 'Papa Joe Saloon & Steakhouse');

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `ertekelesek`
--

CREATE TABLE `ertekelesek` (
  `Id` int(11) NOT NULL,
  `FelhasznaloId` int(11) NOT NULL,
  `EtteremId` int(11) NOT NULL,
  `Szoveg` varchar(255) NOT NULL,
  `Ertekeles` int(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `etelek`
--

CREATE TABLE `etelek` (
  `Id` int(11) NOT NULL,
  `nev` varchar(32) NOT NULL,
  `kaloria` int(11) NOT NULL,
  `ar` int(11) NOT NULL,
  `Learazas` int(3) NOT NULL,
  `ChainId` int(11) NOT NULL,
  `Indexkep` varchar(224) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- A tábla adatainak kiíratása `etelek`
--

INSERT INTO `etelek` (`Id`, `nev`, `kaloria`, `ar`, `Learazas`, `ChainId`, `Indexkep`) VALUES
(1, ' Big Tasty® ', 903, 3190, 30, 3, 'https://s7d1.scene7.com/is/image/mcdonalds/NAT253_BigTasty_Termekkep_832x822_v2:nutrition-calculator-tile?wid=822&hei=822&dpr=off'),
(2, 'Big Mac® McMenü® Plusz', 965, 2590, 0, 3, 'https://s7d1.scene7.com/is/image/mcdonalds/DIG_154_Menu_McMenuPlusz_832x822:nutrition-calculator-tile?wid=822&hei=822&dpr=off'),
(3, 'Málnás-krémtúrós pite', 246, 790, 0, 3, 'https://s7d1.scene7.com/is/image/mcdonalds/AMalnasKremturosPite_Web_832x822:nutrition-calculator-tile?wid=822&hei=822&dpr=off'),
(4, 'McChicken®', 422, 750, 0, 3, 'https://s7d1.scene7.com/is/image/mcdonalds/mcchicken-kep:nutrition-calculator-tile?wid=822&hei=822&dpr=off'),
(5, 'Sajtburger', 326, 750, 0, 3, 'https://s7d1.scene7.com/is/image/mcdonalds/DIG_202_Sajtburger_Termekkep_832x822:nutrition-calculator-tile?wid=822&hei=822&dpr=off'),
(6, 'Sajtburger McMoment® menü', 231, 1340, 0, 3, 'https://s7d1.scene7.com/is/image/mcdonalds/DIG_122_Weboldal_Termekkep_Szendvics_Duplasajtburger_jalapeno_832x822:nutrition-calculator-tile'),
(7, 'Bacon King', 1288, 3220, 0, 0, 'https://burgerking.hu/app/uploads/2024/09/BaconKing_690x387px.png'),
(8, 'Dupla WHOPPER®', 1059, 2490, 0, 0, 'https://www.burgerking.ee/images/optimized/products/dbl-whopper-desktop-9a3769cafad2eb0eb9c40e583b5a6642.png'),
(9, 'WHOPPER® Jr.', 368, 1390, 0, 0, 'https://www.burgerking.ee/images/optimized/products/whopper-jr-desktop-1fa41c48157077fcd15ef66636aa8466.png'),
(10, 'Chicken Bacon King', 773, 2870, 0, 0, 'https://burgerking.hu/app/uploads/2024/09/ChickenBaconKing_690x387px.png'),
(11, '25 kosár', 208, 4990, 0, 2, 'https://sawepecomcdn.blob.core.windows.net/kfc-web-ordering/KFC_HUN/16_Kosarak/440x440/25_bucket_440x440.png'),
(12, 'Piedone pizza(32 cm)', 1148, 3390, 0, 7, 'https://images.deliveryhero.io/image/fd-hu/Products/22672056.jpg'),
(14, 'Chili pizza(32cm)', 113, 3390, 0, 7, 'https://images.deliveryhero.io/image/fd-hu/Products/22672057.jpg'),
(15, 'Pulled pork burger', 280, 2790, 0, 7, 'https://images.deliveryhero.io/image/fd-hu/Products/22672072.jpg'),
(16, 'Box burger', 411, 3990, 0, 7, 'https://images.deliveryhero.io/image/fd-hu/Products/22672098.jpg'),
(19, 'quesadillas chicken', 111, 2690, 0, 7, 'https://images.deliveryhero.io/image/fd-hu/Products/22697845.jpg'),
(20, 'Amerikai hot-dog', 476, 1590, 0, 7, 'https://images.deliveryhero.io/image/fd-hu/Products/22696768.jpg'),
(21, 'Hasábburgonya', 172, 990, 0, 7, 'https://images.deliveryhero.io/image/fd-hu/Products/22672108.jpg'),
(22, 'Rántott hagymakarika', 480, 990, 0, 7, 'https://images.deliveryhero.io/image/fd-hu/Products/22672121.jpg'),
(23, 'Majonézes tojássaláta', 192, 1590, 0, 7, 'https://images.deliveryhero.io/image/fd-hu/Products/22672124.jpg'),
(24, 'Fokhagymás, joghurtos szósz', 101, 450, 0, 7, 'https://images.deliveryhero.io/image/fd-hu/Products/22672130.jpg'),
(25, 'Tartárszósz', 41, 450, 0, 7, 'https://images.deliveryhero.io/image/fd-hu/Products/22672132.jpg'),
(26, 'Csípős szósz', 122, 450, 0, 7, 'https://images.deliveryhero.io/image/fd-hu/Products/22672133.jpg'),
(27, 'Csokis marlenka', 994, 1250, 0, 7, 'https://images.deliveryhero.io/image/fd-hu/Products/24404109.jpg'),
(28, 'Coca.Cola 500ml', 42, 800, 0, 7, 'https://images.deliveryhero.io/image/fd-hu/Products/22672135.jpg'),
(29, 'Fanta Narancs 500ml', 28, 800, 0, 7, 'https://images.deliveryhero.io/image/fd-hu/Products/22672137.jpg'),
(30, 'Szénsavas ásványvíz', 0, 550, 0, 7, 'https://images.deliveryhero.io/image/fd-hu/Products/22672190.jpg'),
(31, 'Soproni sör (0,5l)', 136, 1200, 0, 7, 'https://images.deliveryhero.io/image/fd-hu/Products/22672138.jpg'),
(32, 'Pikáns leves', 267, 1490, 0, 8, ''),
(33, 'Tenger gyümölcsei leves', 240, 1690, 0, 8, ''),
(34, 'Miso (tofu) leves', 131, 1390, 0, 8, ''),
(35, 'Zöldségleves', 78, 1390, 0, 8, ''),
(36, 'Gomba sütve', 26, 1390, 0, 8, ''),
(37, 'Garnélanyárs salátaágyon', 85, 1590, 0, 8, ''),
(38, 'Szójacsíra saláta', 120, 1390, 0, 8, ''),
(39, 'Uborkasaláta', 53, 1590, 0, 8, ''),
(40, 'Ropogós kacsa', 135, 4490, 0, 8, ''),
(41, 'Kacsa Rahmen', 62, 3290, 0, 8, ''),
(42, 'Jázmin rizs', 320, 990, 0, 8, ''),
(43, 'Lychee kompót', 66, 1690, 0, 8, ''),
(44, 'Maguro Set', 118, 4490, 0, 8, ''),
(45, 'Sushibox', 200, 9990, 0, 8, ''),
(46, 'Margherita - Sajtos pizza', 179, 2800, 0, 6, ''),
(47, 'Cippola - Hagymás pizza', 179, 2950, 0, 8, ''),
(48, 'Prosciutto - Sonkás pizza', 179, 2900, 0, 6, ''),
(49, 'Speziale - Speciál pizza', 137, 3600, 0, 6, ''),
(50, 'Corvinus pizza', 178, 3600, 0, 6, ''),
(51, 'Bóbitás köcsögleves', 137, 2450, 0, 6, ''),
(52, 'Gulyásleves csészében', 139, 1600, 0, 6, ''),
(53, 'Ponty-halászlé bográcsban', 49, 4300, 0, 6, ''),
(54, 'Bolognai spagetti', 137, 3500, 0, 6, ''),
(55, 'Túrós csusza tepertővel', 137, 3500, 0, 6, ''),
(56, 'Töltött káposzta', 139, 3800, 0, 6, ''),
(57, 'Sertéspörkölt galuskával', 139, 4100, 0, 6, ''),
(58, 'Chef salátatál', 137, 3600, 0, 6, ''),
(59, 'Görög saláta', 177, 3100, 0, 6, ''),
(60, 'Tonhalas saláta', 144, 3500, 0, 6, ''),
(61, 'Somlói galuska', 178, 1650, 0, 6, ''),
(62, 'Gesztenyepüré tejszinhabbal', 177, 1500, 0, 6, ''),
(63, 'feltét nélkül', 350, 990, 0, 5, 'https://s3.eu-central-1.amazonaws.com/onemin-prod-frankfurt/products/images/000/242/423/original/20190921_173842.jpg'),
(64, 'Tejfölös', 500, 1690, 0, 5, ''),
(65, 'Sajtos', 500, 1690, 0, 5, 'https://www.premiumpiacszeged.hu/img/38802/BUF0009/240x240,r/BUF0009.jpg'),
(66, 'Sajtos-tejfölös', 700, 2000, 0, 5, 'https://s3.eu-central-1.amazonaws.com/onemin-prod-frankfurt/products/images/000/242/425/original/20190921_173201.jpg'),
(67, 'Libaleves grízgaluskával', 133, 1850, 0, 4, 'http://papajoe.hu/wp-content/uploads/2024/12/Papa-Joe-Karacsonyi-ajanlat-leves-7-2048x1368.jpg'),
(69, 'Libacomb', 177, 6850, 0, 4, 'http://papajoe.hu/wp-content/uploads/2024/12/Liba-2048x1368.jpg'),
(70, 'Gundel palacsinta', 185, 1950, 0, 4, 'http://papajoe.hu/wp-content/uploads/2024/12/palacsinta-2048x1368.jpg'),
(71, 'Classic kosár', 1294, 5290, 0, 2, 'https://ocs-pl.oktawave.com/v1/AUTH_876e5729-f8dd-45dd-908f-35d8bb716177/amrest-web-ordering/KFC_HUN/16_Kosarak_uj_palast/440x440/kfc_hun_classic_bucket_440x440.png'),
(72, 'Shake Deluxe: Édes és Epres Nagy', 319, 1500, 0, 2, 'https://sawepecomcdn.blob.core.windows.net/kfc-web-ordering/KFC_HUN/10_Desszertek/440x440/kfc_hun_strawberry_shake_500_440x440.png'),
(73, 'Csokis Amerikai Palacsinta', 638, 1315, 0, 2, 'https://sawepecomcdn.blob.core.windows.net/kfc-web-ordering/KFC_HUN/10_Desszertek/440x440/kfc_csokis_palacsinta_440x440.png'),
(74, 'Mac&Cheese Lunchbox Menü', 755, 3290, 0, 2, 'https://sawepecomcdn.blob.core.windows.net/kfc-web-ordering/KFC_HUN/18_Nagyon_Sajtos/Dine_in/440x440/2025_w1_macncheese_Menu_box_440.png'),
(75, 'Kentucky Gold Grander', 688, 3190, 0, 2, 'https://sawepecomcdn.blob.core.windows.net/kfc-web-ordering/KFC_HUN/31_Kentucky_Gold/Delivery/440x440/kfc_hun_KG_grander_440x440.png'),
(76, 'Twister Classic', 565, 2090, 0, 2, 'https://sawepecomcdn.blob.core.windows.net/kfc-web-ordering/KFC_HUN/22_Wraps/440x440/kfc_hun_twister_440x440.jpg'),
(77, 'Sajtburger Gyerek Menü', 454, 2390, 0, 2, 'https://sawepecomcdn.blob.core.windows.net/kfc-web-ordering/KFC_HUN/12_Gyerek_menu/440x440/kfc_kids_menu_cheeseburger_440x440.png'),
(78, 'Poké Bowl New Mexico Menü', 590, 3390, 0, 2, 'https://sawepecomcdn.blob.core.windows.net/kfc-web-ordering/KFC_HUN/19_Poke/440x440/kfc_poke_bowl_new_mexico_delivery_440x440.png'),
(79, 'Sült Burgonya Kosár', 340, 1490, 0, 2, 'https://sawepecomcdn.blob.core.windows.net/kfc-web-ordering/KFC_HUN/15_Koretek/440x440/kfc_burgonykosar_web_440x440.png'),
(80, 'Lipton Zöld Tea', 0, 880, 0, 2, 'https://sawepecomcdn.blob.core.windows.net/kfc-web-ordering/KFC_HUN/14_Italok/440x440/kfc_dsr_lipton_green_tea_033_can_440x440.png'),
(81, 'Pepsi (0,33l)', 139, 880, 0, 2, 'https://sawepecomcdn.blob.core.windows.net/kfc-web-ordering/KFC_HUN/14_Italok/440x440/kfc_dsr_pepsi_033_can_440x440.png'),
(82, 'Pepsi Max (0,33l)', 0, 880, 0, 2, 'https://sawepecomcdn.blob.core.windows.net/kfc-web-ordering/KFC_HUN/14_Italok/440x440/kfc_dsr_pepsi_zero_sugar_033_can_440x440-min.png'),
(83, 'Pepsi (1l)', 102, 1140, 0, 2, 'https://sawepecomcdn.blob.core.windows.net/kfc-web-ordering/KFC_HUN/14_Italok/440x440/kfc_hu_pepsi_1l_440x440.png'),
(84, 'Topjoy Rostos Narancslé (0,3l)', 112, 880, 0, 2, 'https://sawepecomcdn.blob.core.windows.net/kfc-web-ordering/KFC_HUN/14_Italok/440x440/kfc_dsr_topjoy_narancs_440x440.png'),
(85, 'Happy Day Almalé (0,2l)', 201, 850, 0, 2, 'https://amrestcdn.azureedge.net/kfc-web-ordering/KFC_HUN/14_Italok/440x440/kfc_happyday_apple_440x440.png'),
(86, 'Szénsavas Ásványvíz (0,5l)', 0, 620, 0, 2, 'https://sawepecomcdn.blob.core.windows.net/kfc-web-ordering/KFC_HUN/14_Italok/440x440/kfc_dsr_szentkiralyi_500_savas_440x440.png'),
(87, 'Baracklé (0,33l)', 54, 880, 0, 2, 'https://sawepecomcdn.blob.core.windows.net/kfc-web-ordering/KFC_HUN/14_Italok/440x440/kfc_dsr_toma_juice_330_peach_440x440.png'),
(88, 'Pepsi max (1l)', 0, 1140, 0, 2, 'https://sawepecomcdn.blob.core.windows.net/kfc-web-ordering/KFC_HUN/14_Italok/440x440/kfc_dsr_pepsi_zero_sugar_1l_440x440.png'),
(89, 'Canada Dry (0,33l)', 90, 880, 0, 2, 'https://sawepecomcdn.blob.core.windows.net/kfc-web-ordering/KFC_HUN/14_Italok/440x440/kfc_dsr_canada_dry_033_can_440x440.png'),
(90, 'Pepsi Max Lime (1l)', 0, 1140, 0, 2, 'https://sawepecomcdn.blob.core.windows.net/kfc-web-ordering/KFC_HUN/14_Italok/440x440/kfc_dsr_pepsi_lime_1l_440x440.png'),
(91, 'Gösser Natur Zitrone ', 0, 1170, 0, 2, 'https://sawepecomcdn.blob.core.windows.net/kfc-web-ordering/KFC_HUN/14_Italok/440x440/KFC_HUN_Gosser_nature_zitrone_440x440.png'),
(92, 'Dreher 24 Alkoholmentes', 0, 1170, 0, 2, 'https://sawepecomcdn.blob.core.windows.net/kfc-web-ordering/KFC_HUN/14_Italok/440x440/KFC_HUN_Dreher_0_5_alkoholmentes_440x440.png'),
(93, 'Mirinda Zero 0,33l', 0, 880, 0, 2, 'https://sawepecomcdn.blob.core.windows.net/kfc-web-ordering/KFC_HUN/14_Italok/440x440/KFC_HUN_mirinda_zero_440x440.png'),
(94, 'Szentkirályi Szénsavmentes', 0, 620, 0, 2, 'https://sawepecomcdn.blob.core.windows.net/kfc-web-ordering/KFC_HUN/14_Italok/440x440/kfc_hun_still_water_05_440x440.png'),
(95, 'Gluténmentes WHOPPER®', 803, 2320, 0, 0, 'https://burgerking.hu/app/uploads/2024/09/GlutenmentesWhopper_690x387px.png'),
(96, 'Vegetáriánus WHOPPER®', 594, 1920, 0, 0, 'https://burgerking.hu/app/uploads/2024/09/VegetarianusWhopper_690x387px.png'),
(97, 'Plant-Based WHOPPER®', 645, 1920, 0, 0, 'https://burgerking.hu/app/uploads/2024/09/Plant-BasedWhopper_690x387px.png'),
(98, 'Gouda Sajtfalatka 5db', 278, 1190, 0, 0, 'https://burgerking.hu/app/uploads/2024/11/GoudaSajtfalatok-5db_690x387px.png'),
(99, 'Gouda Sajtfalatka 8db', 278, 1690, 0, 0, 'https://imageproxy.wolt.com/assets/673c34fe7672b02cd07bc566?w=600'),
(100, 'King Nuggets (6db)', 246, 1780, 0, 0, 'https://imageproxy.wolt.com/menu/menu-images/683be82c-6c3f-11ea-bd1a-0a5864764736_Wolt_KingNuggets6db_1440x810px.jpeg'),
(101, 'King Nuggets(9db)', 370, 1910, 0, 0, 'https://imageproxy.wolt.com/menu/menu-images/6f948868-6c3f-11ea-912a-0a58647df487_Wolt_KingNuggets9db_1440x810px.jpeg'),
(102, 'King Nuggets (20db)', 822, 2750, 0, 0, 'https://imageproxy.wolt.com/menu/menu-images/79aa3afa-6c3f-11ea-869b-0a5864764736_Wolt_KingNuggets20db_1440x810px.jpeg'),
(103, 'Spicy King Nuggets(6db)', 337, 1930, 0, 2, 'https://imageproxy.wolt.com/menu/menu-images/5e74b2bc15f04c0fb6a9ce14/03a07e58-f6a7-11ea-a68d-b63cad5ab626_wolt_spicykingnuggets6db_1szosz_1440x810px.jpeg?w=600'),
(104, 'Spicy King Nuggets(9db)', 506, 2060, 0, 0, 'https://imageproxy.wolt.com/menu/menu-images/5e74b2bc15f04c0fb6a9ce14/182b93a8-f6a7-11ea-b8d3-3286c009ded4_wolt_spicykingnuggets9db_2szosz_1440x810px.jpeg?w=600'),
(105, 'Spicy King Nuggets(20db)', 1125, 2900, 0, 0, 'https://imageproxy.wolt.com/menu/menu-images/5e74b2bc15f04c0fb6a9ce14/2bdaffc4-f6a7-11ea-b0ad-7a2f2e31cbb0_wolt_spicykingnuggets20db_4szosz_1440x810px.jpeg?w=600'),
(106, 'Pöttyös King Fusion', 346, 1490, 0, 0, 'https://burgerking.hu/app/uploads/2024/09/PottyosKingFusion_690x387px.png'),
(107, 'Oreo® King Fusion', 280, 1490, 0, 0, 'https://burgerking.hu/app/uploads/2024/09/OreoKingFusion_690x387px.png'),
(108, 'Epres-kekszes King Fusion', 325, 1490, 0, 0, 'https://burgerking.hu/app/uploads/2024/09/Epres-kekszesFusion_690x387px-690x387.png'),
(109, 'Brownie Cheesecake', 291, 1040, 0, 0, 'https://burgerking.hu/app/uploads/2024/09/BrownieSajttorta_690x387px.png'),
(110, 'Mini Palacsinta', 307, 870, 0, 0, 'https://burgerking.hu/app/uploads/2024/09/MiniPalacsinta_690x387px.png'),
(123, 'Coca-Cola®', 180, 870, 0, 0, 'https://burgerking.hu/app/uploads/2024/09/Cola_690x387px.png'),
(124, 'Coca-Cola® Zero', 0, 870, 0, 0, 'https://burgerking.hu/app/uploads/2024/09/ColaZero_690x387px.png'),
(125, 'Sió Almalé', 86, 880, 0, 0, 'https://burgerking.hu/app/uploads/2024/09/Sio-alma_02_690x387px.png'),
(126, 'Red Bull', 110, 850, 0, 0, 'https://burgerking.hu/app/uploads/2024/09/RedBull_690x387px.png'),
(127, 'Szénsavmentes Ásványvíz', 0, 670, 0, 0, 'https://burgerking.hu/app/uploads/2024/09/NaturAqua-mentes_690x387px.png'),
(128, ' Filet-O-Fish® ', 313, 1710, 0, 3, 'https://s7d1.scene7.com/is/image/mcdonalds/DIG_154_FileOFish_Termekkep_832x822:product-header-desktop?wid=829&hei=455&dpr=off'),
(129, 'McWrap® ', 541, 2290, 0, 3, 'https://s7d1.scene7.com/is/image/mcdonalds/DIG254_Weboldal_Termekkep_CrispyWrapBundas_832x822:product-header-desktop?wid=829&hei=455&dpr=off');

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `ettermek`
--

CREATE TABLE `ettermek` (
  `Id` int(11) NOT NULL,
  `Cim` varchar(32) NOT NULL,
  `ChainId` int(11) NOT NULL,
  `varosId` int(11) NOT NULL,
  `Indexkep` varchar(224) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- A tábla adatainak kiíratása `ettermek`
--

INSERT INTO `ettermek` (`Id`, `Cim`, `ChainId`, `varosId`, `Indexkep`) VALUES
(2, 'József krt. 8', 3, 1, 'https://lh3.googleusercontent.com/p/AF1QipPo--v0GPx6WcEF30wtilnC3SriT_pJmsArFAzZ=s184-w184-h130-n-k-no'),
(3, 'Kishegyesi út 2', 3, 3, 'https://lh3.googleusercontent.com/p/AF1QipPehHPiyy1exc91j_MZ7VigDIXKfJeG8GuYQtfk=s156-w156-h108-n-k-no'),
(4, 'Soltész Nagy Kálmán u. 152', 3, 2, 'https://lh3.googleusercontent.com/p/AF1QipMa4m2zd4QtjrRT5wn8u6SfKQtaFjA05OlBRCkK=s184-w184-h144-n-k-no'),
(5, 'Baross Gábor út 23', 3, 4, 'https://lh3.googleusercontent.com/p/AF1QipP0QD6cNrHITN5gvCwnZuMURpuL_lcZUD5zSaPa=s184-w184-h144-n-k-no'),
(6, 'Jókai u. 46', 3, 7, 'https://lh3.googleusercontent.com/p/AF1QipPWMjYWIjFUV8SX2B4y5JI7EG7DonlVErwNcOjL=s184-w184-h144-n-k-no'),
(7, 'László u. 59', 3, 10, 'https://lh3.googleusercontent.com/p/AF1QipM_lhOBkKiVkcfpxYjyd7xl79POvWVP5x-RWDyN=s184-w184-h144-n-k-no'),
(8, 'Kéthly Anna u. 7', 3, 5, 'https://lh3.googleusercontent.com/p/AF1QipMbYVk04B4aZxQT5w7X3ga55eh2_n-zmNNPLKfC=s184-w184-h144-n-k-no'),
(9, 'Omszk park 1', 3, 11, 'https://lh5.googleusercontent.com/p/AF1QipMawBGqYqKBhQ8zA9gY6YUrXlaWfBCyYl9cqTL_=w650-h315-k-no'),
(10, 'Garibaldi u. 2', 3, 12, 'https://lh3.googleusercontent.com/p/AF1QipNoSTJ2LsCKL1a7cp1wysXRtAz3yqwqk_CZVu5a=s184-w184-h144-n-k-no'),
(11, 'Bossányi Krisztina utca 13', 3, 13, 'https://static.regon.hu/pe/2019/08/meki.jpg'),
(12, 'Váci u 7', 0, 1, 'https://lh3.googleusercontent.com/p/AF1QipNH6WFSxoono1y8NmPUHIPR3zFkgG57tm6NwoGF=s138-w138-h108-n-k-no'),
(13, 'Böszörményi út 24', 0, 3, 'https://lh3.googleusercontent.com/p/AF1QipMTHX3eCUi7lf6lz9fbKB4rcTFQ_4x8JwiJ7Z4A=s120-w120-h87-n-k-no'),
(14, 'Budai út 1', 0, 4, 'https://www.arkadgyor.hu/fileadmin/_processed_/c/4/csm_2023_01_24_Burger_King_04_ac3edae6c1.jpg'),
(15, 'Dunaföldvári út 2', 0, 7, 'https://kecskemet.hu/assets/cache/images/fe/fe7ecb45eab5e6b76f94c50e19c6b17f.jpg'),
(16, 'József Attila u. 87', 0, 2, 'https://lh3.googleusercontent.com/p/AF1QipNmDItCdrK17ptK-cdbucv6G3Vx4egmmQT0QR7S=s184-w184-h144-n-k-no'),
(17, 'Pazonyi út 32', 0, 10, 'https://lh3.googleusercontent.com/p/AF1QipPW_R-P0PC6ex8fF-w1GfRTIbe_LdewK1sruvhW=w600-k'),
(18, 'Sport u. 2', 0, 12, 'https://www.ittjartam.hu/profil/ugor-images/burger-king-auchan-budaors-964-1200x800.webp'),
(19, 'Határdomb út 1-3', 0, 6, 'https://i2.wp.com/cyberpress.hu/wp-content/uploads/2021/06/Burger_King_atado-5-Nagy.jpg?fit=1200%2C900&ssl=1'),
(20, 'Lackner K. u 60', 3, 6, 'https://ikvahir.eu/wp-content/uploads/2022/04/20220412_193840.jpg'),
(21, 'Ipar krt. 32', 2, 6, 'https://ikvahir.eu/wp-content/uploads/2018/10/IMG_20181008_181229-Egyedi.jpg'),
(22, 'Várkerület 108', 4, 6, 'https://dynamic-media-cdn.tripadvisor.com/media/photo-o/06/bb/10/18/papa-joe-s-saloon-steakhouse.jpg?w=900&h=500&s=1'),
(23, 'Hajnal tér 23', 5, 6, 'https://etterem.hu/img/x/p9853n/1415026264-6370.jpg'),
(24, 'Fő tér 8', 6, 6, 'https://corvinusetterem.hu/images/microsites/1920x716/2b0c6d9e.jpg'),
(25, 'Mátyás király u. 1', 7, 6, 'https://maimoni.cafeblog.hu/files/2012/08/DSC05682.jpg'),
(26, 'Határdomb út 1-2', 8, 6, 'https://etterem.hu/img/max960/p11984n/1425302845-6304.jpg'),
(27, 'Thököly út 6', 2, 1, 'https://lh3.googleusercontent.com/p/AF1QipOtVUysO953eLLPNtUSsiRk34lxhXqlY3_Lcs2s=s138-w138-h108-n-k-no'),
(28, 'Pesti út 13', 2, 2, 'https://lh3.googleusercontent.com/p/AF1QipN6oMeRZtKqttcjT1QqkEAXrP7Dx38_4HpZh3G2=s90-w90-h72-n-k-no'),
(30, 'Kishegyesi út 1', 2, 3, 'https://lh3.googleusercontent.com/p/AF1QipPTiR13rDZPnUEkEo0DviEcllk_rsDUBu6a2FXM=s90-w90-h72-n-k-no'),
(31, 'Baross Gábor út 24', 2, 4, 'https://lh3.googleusercontent.com/p/AF1QipNHs0vguWvzY-yqHBIHCe0PJvHFe2bhQNzYMfAo=s90-w90-h72-n-k-no'),
(32, 'Izsáki út 12', 2, 7, 'https://lh3.googleusercontent.com/p/AF1QipN_kX9V9ZsBdzMbncfh5CGuS0IeFOHQAY92B_M-=s184-w184-h144-n-k-no'),
(33, 'Kert u. 47/A', 2, 10, 'https://lh3.googleusercontent.com/p/AF1QipNM8GkBv-_IPD8LbQtaLyeFUmGMY3Q5BFb2ZCq6=s220-w165-h220-n-k-no'),
(34, 'Garibaldi u. 2', 2, 12, 'https://lh3.googleusercontent.com/p/AF1QipNFvblN-gaSjDez6gFK3-Chzzoryu7P4aU4tBbG=s184-w184-h130-n-k-no'),
(35, 'Dózsa György út 136', 2, 13, 'https://dynamic-media-cdn.tripadvisor.com/media/photo-o/2a/4e/18/f1/caption.jpg?w=900&h=-1&s=1'),
(36, 'Kalászi út HRSZ 11283', 2, 14, 'https://lh5.googleusercontent.com/p/AF1QipNn_ZH-VvQR6bHzftdfa9reJrV4DO2FYzkuFRd4=w650-h486-k-no'),
(37, 'Makay István út 5', 2, 8, 'https://lh3.googleusercontent.com/p/AF1QipM14DMaMwE7HeacrzmQQqC6ZcKWeEusIum5SJMh=s90-w90-h72-n-k-no'),
(38, 'Kárász u. 16', 2, 9, 'https://lh3.googleusercontent.com/p/AF1QipPhxbWHxNvts1G17fgPGLxeB-PM5PZvtAiaLdxE=s120-w120-h87-n-k-no'),
(39, 'Zanati út 70', 2, 5, 'https://lh3.googleusercontent.com/p/AF1QipN7aRg9JGeLbgFdy_S4Rtokde0JPZb9rrw7bnOE=s184-w184-h144-n-k-no');

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `excludedetel`
--

CREATE TABLE `excludedetel` (
  `EtelId` int(11) NOT NULL,
  `EtteremId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- A tábla adatainak kiíratása `excludedetel`
--

INSERT INTO `excludedetel` (`EtelId`, `EtteremId`) VALUES
(4, 2),
(9, 13);

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `felhasznalok`
--

CREATE TABLE `felhasznalok` (
  `Id` int(255) NOT NULL,
  `FelhasznaloNev` varchar(32) NOT NULL,
  `TeljesNev` varchar(64) NOT NULL,
  `Email` varchar(64) NOT NULL,
  `VarosId` int(11) NOT NULL,
  `Lakcim` varchar(64) NOT NULL,
  `Hash` varchar(64) NOT NULL,
  `Salt` varchar(64) NOT NULL,
  `Jogosultsag` int(11) NOT NULL,
  `Aktiv` int(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- A tábla adatainak kiíratása `felhasznalok`
--

INSERT INTO `felhasznalok` (`Id`, `FelhasznaloNev`, `TeljesNev`, `Email`, `VarosId`, `Lakcim`, `Hash`, `Salt`, `Jogosultsag`, `Aktiv`) VALUES
(7, 'TakacsL', 'Takacs Laszlo', 'takacslacika81@gmail.com', 1, 'Utca 29', '526100240222f74303ededea5e0e631e8682a9df6904e191c2b10857e41ce99c', 'tboBWFwyJUUDZRTlFgPIvkc4J8GygbyOhiE1US5Cg9WIceyofDMTQNInaUTavyop', 0, 1),
(8, 'timike', 'asd1234', 'timkoa@kkszki.hu', 1, 'Otthon utca 24', '488a4ab254ce938e799f172d6071fb00fded4c4a8110f01c5ba49c4a5bb7bbd8', 'lZQoFxAS2e9gj6CNbnAr7yXT6oBV4QCsBMphTyy5fi9TgkrXh4cLJkenvdVb', 2, 1);

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `learazas`
--

CREATE TABLE `learazas` (
  `EtteremId` int(11) NOT NULL,
  `EtelId` int(11) NOT NULL,
  `Learazas` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- A tábla adatainak kiíratása `learazas`
--

INSERT INTO `learazas` (`EtteremId`, `EtelId`, `Learazas`) VALUES
(13, 8, 30),
(21, 11, 50);

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `rendeles`
--

CREATE TABLE `rendeles` (
  `Id` int(11) NOT NULL,
  `FelhasznaloId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- A tábla adatainak kiíratása `rendeles`
--

INSERT INTO `rendeles` (`Id`, `FelhasznaloId`) VALUES
(2, 7);

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `rendeltetel`
--

CREATE TABLE `rendeltetel` (
  `EtelId` int(11) NOT NULL,
  `RendelesId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- A tábla adatainak kiíratása `rendeltetel`
--

INSERT INTO `rendeltetel` (`EtelId`, `RendelesId`) VALUES
(1, 2),
(51, 2),
(94, 2);

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `varosok`
--

CREATE TABLE `varosok` (
  `Id` int(11) NOT NULL,
  `Nev` varchar(32) NOT NULL,
  `indexKep` varchar(224) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- A tábla adatainak kiíratása `varosok`
--

INSERT INTO `varosok` (`Id`, `Nev`, `indexKep`) VALUES
(1, 'Budapest', 'https://a.eu.mktgcdn.com/f/100004519/5QlR-I7B4XyrnISxsKAEEu-wWKUlrRii4yA9x1-4nlc.jpg'),
(2, 'Miskolc', 'https://travelking.cdn.it7.sk/deadpool/deal-listing/468/9530-oVg5.jpg'),
(3, 'Debrecen', 'https://upload.wikimedia.org/wikipedia/commons/thumb/d/d9/Debrecen_f%C5%91t%C3%A9r_2009.jpg/1200px-Debrecen_f%C5%91t%C3%A9r_2009.jpg'),
(4, 'Győr', 'https://admissions.sze.hu/images/Photos/gyor-transformed-transformed.jpeg'),
(5, 'Szombathely', 'https://static.345.hu/varoskepek/dc5a7f270d71b419b92150459bff9ddb.jpg'),
(6, 'Sopron', 'https://upload.wikimedia.org/wikipedia/commons/5/50/Storno-h%C3%A1z_T%C5%B1ztorony_%C3%A9s_V%C3%A1rosh%C3%A1za.jpg'),
(7, 'Kecskemét', 'https://upload.wikimedia.org/wikipedia/commons/6/6b/V%C3%A1rosi_Tan%C3%A1csh%C3%A1z_%282253._sz%C3%A1m%C3%BA_m%C5%B1eml%C3%A9k%29_4.jpg'),
(8, 'Pécs', 'https://traveladdict.hu/wp-content/uploads/2021/08/IMG_9648.jpg'),
(9, 'Szeged', 'https://www.szeretlekmagyarorszag.hu/wp-content/uploads/2019/07/szeged-szegedcity-suncity-helloszeged-iloveszeged-50-1.jpg'),
(10, 'Nyíregyháza', 'https://mihalygabor-utazasai.hu/wp-content/uploads/2020/10/IMG_2023-07-25-093029-1024x768.jpeg'),
(11, 'Budakalász', 'https://www.pilisinfo.hu/imagebase/4a14a444/11062358_784192214983981_3585371202711634487_n.jpg'),
(12, 'Budaörs', 'https://i.szalas.hu/pois/8325/500x500/28268.jpg'),
(13, 'Gödöllő', 'https://upload.wikimedia.org/wikipedia/commons/thumb/f/f7/Schloss_G%C3%B6d%C3%B6ll%C5%91_Ungarn.jpg/1200px-Schloss_G%C3%B6d%C3%B6ll%C5%91_Ungarn.jpg'),
(14, 'Szentendre', 'https://felfedezok.hu/wp-content/uploads/2023/05/city-g58e10c678_1280-e1685002574358.jpg');

--
-- Indexek a kiírt táblákhoz
--

--
-- A tábla indexei `chain`
--
ALTER TABLE `chain`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `Nev` (`Nev`);

--
-- A tábla indexei `ertekelesek`
--
ALTER TABLE `ertekelesek`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `FelhasznaloId` (`FelhasznaloId`,`EtteremId`),
  ADD KEY `EtteremId` (`EtteremId`);

--
-- A tábla indexei `etelek`
--
ALTER TABLE `etelek`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `nev` (`nev`),
  ADD KEY `etteremId` (`ChainId`);

--
-- A tábla indexei `ettermek`
--
ALTER TABLE `ettermek`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `varosId` (`varosId`),
  ADD KEY `ChainId` (`ChainId`);

--
-- A tábla indexei `excludedetel`
--
ALTER TABLE `excludedetel`
  ADD PRIMARY KEY (`EtelId`,`EtteremId`),
  ADD KEY `EtelId` (`EtelId`,`EtteremId`),
  ADD KEY `EtteremId` (`EtteremId`);

--
-- A tábla indexei `felhasznalok`
--
ALTER TABLE `felhasznalok`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `Email` (`Email`),
  ADD UNIQUE KEY `FelhasznaloNev` (`FelhasznaloNev`),
  ADD KEY `VarosId` (`VarosId`);

--
-- A tábla indexei `learazas`
--
ALTER TABLE `learazas`
  ADD PRIMARY KEY (`EtelId`,`EtteremId`),
  ADD UNIQUE KEY `EtteremId` (`EtteremId`,`EtelId`);

--
-- A tábla indexei `rendeles`
--
ALTER TABLE `rendeles`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `FelhasznaloId_2` (`FelhasznaloId`),
  ADD KEY `FelhasznaloId` (`FelhasznaloId`);

--
-- A tábla indexei `rendeltetel`
--
ALTER TABLE `rendeltetel`
  ADD PRIMARY KEY (`EtelId`,`RendelesId`),
  ADD KEY `EtelId` (`EtelId`,`RendelesId`),
  ADD KEY `RendelesId` (`RendelesId`);

--
-- A tábla indexei `varosok`
--
ALTER TABLE `varosok`
  ADD PRIMARY KEY (`Id`);

--
-- A kiírt táblák AUTO_INCREMENT értéke
--

--
-- AUTO_INCREMENT a táblához `chain`
--
ALTER TABLE `chain`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- AUTO_INCREMENT a táblához `ertekelesek`
--
ALTER TABLE `ertekelesek`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT a táblához `etelek`
--
ALTER TABLE `etelek`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=131;

--
-- AUTO_INCREMENT a táblához `ettermek`
--
ALTER TABLE `ettermek`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=41;

--
-- AUTO_INCREMENT a táblához `felhasznalok`
--
ALTER TABLE `felhasznalok`
  MODIFY `Id` int(255) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- AUTO_INCREMENT a táblához `rendeles`
--
ALTER TABLE `rendeles`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT a táblához `varosok`
--
ALTER TABLE `varosok`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=15;

--
-- Megkötések a kiírt táblákhoz
--

--
-- Megkötések a táblához `ertekelesek`
--
ALTER TABLE `ertekelesek`
  ADD CONSTRAINT `ertekelesek_ibfk_1` FOREIGN KEY (`FelhasznaloId`) REFERENCES `felhasznalok` (`Id`),
  ADD CONSTRAINT `ertekelesek_ibfk_2` FOREIGN KEY (`EtteremId`) REFERENCES `ettermek` (`Id`);

--
-- Megkötések a táblához `etelek`
--
ALTER TABLE `etelek`
  ADD CONSTRAINT `etelek_ibfk_1` FOREIGN KEY (`ChainId`) REFERENCES `chain` (`Id`);

--
-- Megkötések a táblához `ettermek`
--
ALTER TABLE `ettermek`
  ADD CONSTRAINT `ettermek_ibfk_1` FOREIGN KEY (`varosId`) REFERENCES `varosok` (`Id`),
  ADD CONSTRAINT `ettermek_ibfk_2` FOREIGN KEY (`ChainId`) REFERENCES `chain` (`Id`);

--
-- Megkötések a táblához `excludedetel`
--
ALTER TABLE `excludedetel`
  ADD CONSTRAINT `excludedetel_ibfk_1` FOREIGN KEY (`EtteremId`) REFERENCES `ettermek` (`Id`),
  ADD CONSTRAINT `excludedetel_ibfk_2` FOREIGN KEY (`EtelId`) REFERENCES `etelek` (`Id`);

--
-- Megkötések a táblához `felhasznalok`
--
ALTER TABLE `felhasznalok`
  ADD CONSTRAINT `felhasznalok_ibfk_1` FOREIGN KEY (`VarosId`) REFERENCES `varosok` (`Id`);

--
-- Megkötések a táblához `learazas`
--
ALTER TABLE `learazas`
  ADD CONSTRAINT `learazas_ibfk_1` FOREIGN KEY (`EtteremId`) REFERENCES `ettermek` (`Id`),
  ADD CONSTRAINT `learazas_ibfk_2` FOREIGN KEY (`EtelId`) REFERENCES `etelek` (`Id`);

--
-- Megkötések a táblához `rendeles`
--
ALTER TABLE `rendeles`
  ADD CONSTRAINT `rendeles_ibfk_1` FOREIGN KEY (`FelhasznaloId`) REFERENCES `felhasznalok` (`Id`);

--
-- Megkötések a táblához `rendeltetel`
--
ALTER TABLE `rendeltetel`
  ADD CONSTRAINT `rendeltetel_ibfk_1` FOREIGN KEY (`RendelesId`) REFERENCES `rendeles` (`Id`),
  ADD CONSTRAINT `rendeltetel_ibfk_2` FOREIGN KEY (`EtelId`) REFERENCES `etelek` (`Id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
