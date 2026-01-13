/* =========================================================
   KURYE – PAKET TESLİM YÖNETİM SİSTEMİ (SSMS)
   Tek Dosya: DDL + DML + VIEW + SP + MENÜ + DEMO
   Tekrar çalıştırılabilir (Idempotent)
   SQL Server 2017+ uyumlu
   ========================================================= */

------------------------------------------------------------
-- 0) VERİTABANI
------------------------------------------------------------
IF DB_ID(N'KuryeTeslimDB') IS NULL
    CREATE DATABASE KuryeTeslimDB;
GO

USE KuryeTeslimDB;
GO

------------------------------------------------------------
-- 1) TEMEL TABLOLAR (DDL)
------------------------------------------------------------
IF OBJECT_ID(N'dbo.Rol', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Rol (
        RolID INT IDENTITY PRIMARY KEY,
        RolAdi NVARCHAR(50) NOT NULL UNIQUE
    );
END
GO

IF OBJECT_ID(N'dbo.Sube', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Sube (
        SubeID INT IDENTITY PRIMARY KEY,
        SubeAdi NVARCHAR(100) NOT NULL,
        Sehir NVARCHAR(50) NOT NULL
    );
END
GO

IF OBJECT_ID(N'dbo.Kullanici', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Kullanici (
        KullaniciID INT IDENTITY PRIMARY KEY,
        Ad NVARCHAR(50) NOT NULL,
        Soyad NVARCHAR(50) NOT NULL,
        RolID INT NOT NULL,
        CONSTRAINT FK_Kullanici_Rol FOREIGN KEY (RolID) REFERENCES dbo.Rol(RolID)
    );
END
GO

IF OBJECT_ID(N'dbo.PaketDurum', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.PaketDurum (
        DurumID INT IDENTITY PRIMARY KEY,
        DurumAdi NVARCHAR(50) NOT NULL UNIQUE
    );
END
GO

IF OBJECT_ID(N'dbo.Paket', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Paket (
        PaketID INT IDENTITY PRIMARY KEY,
        TakipNo NVARCHAR(30) NOT NULL UNIQUE,
        Gonderen NVARCHAR(100) NOT NULL,
        Alici NVARCHAR(100) NOT NULL,
        CikisSubeID INT NOT NULL,
        DurumID INT NOT NULL,
        Ucret DECIMAL(10,2) NOT NULL,
        OlusturmaTarihi DATETIME NOT NULL DEFAULT GETDATE(),
        CONSTRAINT FK_Paket_Sube FOREIGN KEY (CikisSubeID) REFERENCES dbo.Sube(SubeID),
        CONSTRAINT FK_Paket_Durum FOREIGN KEY (DurumID) REFERENCES dbo.PaketDurum(DurumID)
    );
END
GO

IF OBJECT_ID(N'dbo.PaketHareket', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.PaketHareket (
        HareketID INT IDENTITY PRIMARY KEY,
        PaketID INT NOT NULL,
        TarihSaat DATETIME NOT NULL DEFAULT GETDATE(),
        Aciklama NVARCHAR(200) NOT NULL,
        CONSTRAINT FK_PaketHareket_Paket FOREIGN KEY (PaketID) REFERENCES dbo.Paket(PaketID)
    );
END
GO

------------------------------------------------------------
-- 2) EK TABLO: KuryeAtama
------------------------------------------------------------
IF OBJECT_ID(N'dbo.KuryeAtama', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.KuryeAtama (
        AtamaID INT IDENTITY PRIMARY KEY,
        PaketID INT NOT NULL,
        KuryeID INT NOT NULL,
        AtamaTarihi DATETIME NOT NULL DEFAULT GETDATE(),
        Notlar NVARCHAR(200) NULL,
        CONSTRAINT FK_KuryeAtama_Paket FOREIGN KEY (PaketID) REFERENCES dbo.Paket(PaketID),
        CONSTRAINT FK_KuryeAtama_Kullanici FOREIGN KEY (KuryeID) REFERENCES dbo.Kullanici(KullaniciID)
    );
END
GO

------------------------------------------------------------
-- 3) TÜRKİYE ŞEHİR MODÜLÜ (81 il) + Şube entegrasyonu
------------------------------------------------------------
IF OBJECT_ID(N'dbo.TurkiyeSehir', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.TurkiyeSehir(
        SehirID INT IDENTITY PRIMARY KEY,
        SehirAdi NVARCHAR(50) NOT NULL UNIQUE,
        PlakaKodu INT NULL
    );
END
GO

IF NOT EXISTS (SELECT 1 FROM dbo.TurkiyeSehir)
BEGIN
    INSERT INTO dbo.TurkiyeSehir(SehirAdi, PlakaKodu) VALUES
    (N'Adana',1),(N'Adıyaman',2),(N'Afyonkarahisar',3),(N'Ağrı',4),(N'Amasya',5),
    (N'Ankara',6),(N'Antalya',7),(N'Artvin',8),(N'Aydın',9),(N'Balıkesir',10),
    (N'Bilecik',11),(N'Bingöl',12),(N'Bitlis',13),(N'Bolu',14),(N'Burdur',15),
    (N'Bursa',16),(N'Çanakkale',17),(N'Çankırı',18),(N'Çorum',19),(N'Denizli',20),
    (N'Diyarbakır',21),(N'Edirne',22),(N'Elazığ',23),(N'Erzincan',24),(N'Erzurum',25),
    (N'Eskişehir',26),(N'Gaziantep',27),(N'Giresun',28),(N'Gümüşhane',29),(N'Hakkari',30),
    (N'Hatay',31),(N'Isparta',32),(N'Mersin',33),(N'İstanbul',34),(N'İzmir',35),
    (N'Kars',36),(N'Kastamonu',37),(N'Kayseri',38),(N'Kırklareli',39),(N'Kırşehir',40),
    (N'Kocaeli',41),(N'Konya',42),(N'Kütahya',43),(N'Malatya',44),(N'Manisa',45),
    (N'Kahramanmaraş',46),(N'Mardin',47),(N'Muğla',48),(N'Muş',49),(N'Nevşehir',50),
    (N'Niğde',51),(N'Ordu',52),(N'Rize',53),(N'Sakarya',54),(N'Samsun',55),
    (N'Siirt',56),(N'Sinop',57),(N'Sivas',58),(N'Tekirdağ',59),(N'Tokat',60),
    (N'Trabzon',61),(N'Tunceli',62),(N'Şanlıurfa',63),(N'Uşak',64),(N'Van',65),
    (N'Yozgat',66),(N'Zonguldak',67),(N'Aksaray',68),(N'Bayburt',69),(N'Karaman',70),
    (N'Kırıkkale',71),(N'Batman',72),(N'Şırnak',73),(N'Bartın',74),(N'Ardahan',75),
    (N'Iğdır',76),(N'Yalova',77),(N'Karabük',78),(N'Kilis',79),(N'Osmaniye',80),
    (N'Düzce',81);
END
GO

IF COL_LENGTH('dbo.Sube','SehirID') IS NULL
BEGIN
    ALTER TABLE dbo.Sube ADD SehirID INT NULL;
    ALTER TABLE dbo.Sube
    ADD CONSTRAINT FK_Sube_TurkiyeSehir FOREIGN KEY (SehirID) REFERENCES dbo.TurkiyeSehir(SehirID);
END
GO

UPDATE s
SET SehirID = ts.SehirID
FROM dbo.Sube s
JOIN dbo.TurkiyeSehir ts ON ts.SehirAdi = s.Sehir
WHERE s.SehirID IS NULL;
GO

;WITH Iller AS (SELECT SehirID, SehirAdi FROM dbo.TurkiyeSehir)
INSERT INTO dbo.Sube(SubeAdi, Sehir, SehirID)
SELECT CONCAT(i.SehirAdi, N' Merkez'), i.SehirAdi, i.SehirID
FROM Iller i
WHERE NOT EXISTS (
    SELECT 1
    FROM dbo.Sube s
    WHERE s.SehirID = i.SehirID AND s.SubeAdi = CONCAT(i.SehirAdi, N' Merkez')
);
GO

------------------------------------------------------------
-- 4) Paket: Varış + Anlık konum kolonları
------------------------------------------------------------
IF COL_LENGTH('dbo.Paket','VarisSubeID') IS NULL
BEGIN
    ALTER TABLE dbo.Paket ADD VarisSubeID INT NULL;
    ALTER TABLE dbo.Paket
    ADD CONSTRAINT FK_Paket_VarisSube FOREIGN KEY (VarisSubeID) REFERENCES dbo.Sube(SubeID);
END
GO

IF COL_LENGTH('dbo.Paket','AnlikSubeID') IS NULL
BEGIN
    ALTER TABLE dbo.Paket ADD AnlikSubeID INT NULL;
    ALTER TABLE dbo.Paket
    ADD CONSTRAINT FK_Paket_AnlikSube FOREIGN KEY (AnlikSubeID) REFERENCES dbo.Sube(SubeID);
END
GO

UPDATE dbo.Paket
SET AnlikSubeID = CikisSubeID
WHERE AnlikSubeID IS NULL;
GO

------------------------------------------------------------
-- 5) VERİLER (DML)
------------------------------------------------------------
IF NOT EXISTS (SELECT 1 FROM dbo.Rol WHERE RolAdi = N'Admin')
    INSERT INTO dbo.Rol (RolAdi) VALUES (N'Admin');

IF NOT EXISTS (SELECT 1 FROM dbo.Rol WHERE RolAdi = N'Kurye')
    INSERT INTO dbo.Rol (RolAdi) VALUES (N'Kurye');

IF NOT EXISTS (SELECT 1 FROM dbo.PaketDurum WHERE DurumAdi = N'Oluşturuldu')
    INSERT INTO dbo.PaketDurum (DurumAdi) VALUES (N'Oluşturuldu');

IF NOT EXISTS (SELECT 1 FROM dbo.PaketDurum WHERE DurumAdi = N'Dağıtımda')
    INSERT INTO dbo.PaketDurum (DurumAdi) VALUES (N'Dağıtımda');

IF NOT EXISTS (SELECT 1 FROM dbo.PaketDurum WHERE DurumAdi = N'Varış Şehrinde')
    INSERT INTO dbo.PaketDurum (DurumAdi) VALUES (N'Varış Şehrinde');

IF NOT EXISTS (SELECT 1 FROM dbo.PaketDurum WHERE DurumAdi = N'Teslim Edildi')
    INSERT INTO dbo.PaketDurum (DurumAdi) VALUES (N'Teslim Edildi');
GO

DECLARE @KuryeRolID INT = (SELECT TOP 1 RolID FROM dbo.Rol WHERE RolAdi = N'Kurye');

IF NOT EXISTS (SELECT 1 FROM dbo.Kullanici WHERE Ad = N'Mehmet' AND Soyad = N'Kurye')
    INSERT INTO dbo.Kullanici (Ad, Soyad, RolID) VALUES (N'Mehmet', N'Kurye', @KuryeRolID);

IF NOT EXISTS (SELECT 1 FROM dbo.Kullanici WHERE Ad = N'Ali' AND Soyad = N'Kurye')
    INSERT INTO dbo.Kullanici (Ad, Soyad, RolID) VALUES (N'Ali', N'Kurye', @KuryeRolID);
GO

------------------------------------------------------------
-- 6) VIEW'LER (DROP + CREATE)  -> 111 hatası olmaz
------------------------------------------------------------
IF OBJECT_ID(N'dbo.vw_PaketOzet', N'V') IS NOT NULL DROP VIEW dbo.vw_PaketOzet;
GO
CREATE VIEW dbo.vw_PaketOzet AS
SELECT
    p.PaketID,
    p.TakipNo,
    p.Gonderen,
    p.Alici,
    sCikis.Sehir AS CikisSehir,
    sVaris.Sehir AS VarisSehir,
    sAnlik.Sehir AS AnlikSehir,
    pd.DurumAdi,
    p.Ucret,
    p.OlusturmaTarihi,
    ka.KuryeID,
    (k.Ad + N' ' + k.Soyad) AS KuryeAdSoyad,
    ka.AtamaTarihi
FROM dbo.Paket p
JOIN dbo.Sube sCikis ON sCikis.SubeID = p.CikisSubeID
LEFT JOIN dbo.Sube sVaris ON sVaris.SubeID = p.VarisSubeID
LEFT JOIN dbo.Sube sAnlik ON sAnlik.SubeID = p.AnlikSubeID
JOIN dbo.PaketDurum pd ON pd.DurumID = p.DurumID
LEFT JOIN dbo.KuryeAtama ka ON ka.PaketID = p.PaketID
LEFT JOIN dbo.Kullanici k ON k.KullaniciID = ka.KuryeID;
GO

IF OBJECT_ID(N'dbo.vw_PaketKonum', N'V') IS NOT NULL DROP VIEW dbo.vw_PaketKonum;
GO
CREATE VIEW dbo.vw_PaketKonum AS
SELECT
    p.PaketID,
    p.TakipNo,
    p.Gonderen,
    p.Alici,
    sCikis.Sehir AS CikisSehir,
    sVaris.Sehir AS VarisSehir,
    sAnlik.Sehir AS AnlikSehir,
    pd.DurumAdi,
    p.Ucret,
    p.OlusturmaTarihi
FROM dbo.Paket p
JOIN dbo.Sube sCikis ON sCikis.SubeID = p.CikisSubeID
LEFT JOIN dbo.Sube sVaris ON sVaris.SubeID = p.VarisSubeID
LEFT JOIN dbo.Sube sAnlik ON sAnlik.SubeID = p.AnlikSubeID
JOIN dbo.PaketDurum pd ON pd.DurumID = p.DurumID;
GO

------------------------------------------------------------
-- 7) PROSEDÜRLER
------------------------------------------------------------

-- Amasya otomatik kurye atama
IF OBJECT_ID(N'dbo.sp_AmasyaOtomatikAtama', N'P') IS NOT NULL DROP PROCEDURE dbo.sp_AmasyaOtomatikAtama;
GO
CREATE PROCEDURE dbo.sp_AmasyaOtomatikAtama
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @AmasyaSubeID INT =
    (
        SELECT TOP 1 SubeID
        FROM dbo.Sube
        WHERE Sehir = N'Amasya' AND SubeAdi = N'Amasya Merkez'
    );

    IF @AmasyaSubeID IS NULL
    BEGIN
        PRINT N'Amasya şubesi bulunamadı.';
        RETURN;
    END

    ;WITH Kuryeler AS (
        SELECT k.KullaniciID
        FROM dbo.Kullanici k
        JOIN dbo.Rol r ON r.RolID = k.RolID
        WHERE r.RolAdi = N'Kurye'
    ),
    Atanmamis AS (
        SELECT p.PaketID
        FROM dbo.Paket p
        WHERE p.CikisSubeID = @AmasyaSubeID
          AND NOT EXISTS (SELECT 1 FROM dbo.KuryeAtama ka WHERE ka.PaketID = p.PaketID)
    )
    INSERT INTO dbo.KuryeAtama (PaketID, KuryeID, Notlar)
    SELECT a.PaketID,
           (SELECT TOP 1 KullaniciID FROM Kuryeler ORDER BY NEWID()),
           N'Amasya bölgesi otomatik atama'
    FROM Atanmamis a;

    PRINT N'Amasya otomatik atama tamamlandı.';
END
GO

-- Paket oluştur (TR)
IF OBJECT_ID(N'dbo.sp_PaketOlusturTR', N'P') IS NOT NULL DROP PROCEDURE dbo.sp_PaketOlusturTR;
GO
CREATE PROCEDURE dbo.sp_PaketOlusturTR
    @TakipNo NVARCHAR(30),
    @Gonderen NVARCHAR(100),
    @Alici NVARCHAR(100),
    @CikisSehir NVARCHAR(50),
    @VarisSehir NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM dbo.Paket WHERE TakipNo = @TakipNo)
    BEGIN
        PRINT CONCAT(N'Bu takip numarası zaten var: ', @TakipNo);
        RETURN;
    END

    DECLARE @CikisSubeID INT = (SELECT TOP 1 SubeID FROM dbo.Sube WHERE Sehir = @CikisSehir AND SubeAdi = CONCAT(@CikisSehir, N' Merkez'));
    DECLARE @VarisSubeID INT = (SELECT TOP 1 SubeID FROM dbo.Sube WHERE Sehir = @VarisSehir AND SubeAdi = CONCAT(@VarisSehir, N' Merkez'));

    IF @CikisSubeID IS NULL OR @VarisSubeID IS NULL
    BEGIN
        PRINT N'Şube bulunamadı. Şehir adlarını TurkiyeSehir tablosundaki gibi yaz.';
        RETURN;
    END

    DECLARE @OluID INT = (SELECT TOP 1 DurumID FROM dbo.PaketDurum WHERE DurumAdi = N'Oluşturuldu');

    DECLARE @CikisPlaka INT = (SELECT TOP 1 PlakaKodu FROM dbo.TurkiyeSehir WHERE SehirAdi = @CikisSehir);
    DECLARE @VarisPlaka INT = (SELECT TOP 1 PlakaKodu FROM dbo.TurkiyeSehir WHERE SehirAdi = @VarisSehir);

    DECLARE @Km INT = (ABS(@CikisPlaka - @VarisPlaka) * 35) + 100;
    DECLARE @Ucret DECIMAL(10,2) = 50 + (@Km * 0.35);

    INSERT INTO dbo.Paket(TakipNo, Gonderen, Alici, CikisSubeID, VarisSubeID, AnlikSubeID, DurumID, Ucret)
    VALUES(@TakipNo, @Gonderen, @Alici, @CikisSubeID, @VarisSubeID, @CikisSubeID, @OluID, @Ucret);

    DECLARE @YeniPaketID INT = SCOPE_IDENTITY();

    INSERT INTO dbo.PaketHareket(PaketID, Aciklama)
    VALUES(@YeniPaketID, CONCAT(N'Paket ', @CikisSehir, N' Merkez şubesinde oluşturuldu. Hedef: ', @VarisSehir));

    IF @CikisSehir = N'Amasya'
        EXEC dbo.sp_AmasyaOtomatikAtama;

    PRINT CONCAT(N'Paket oluşturuldu: ', @TakipNo, N' | Km~', @Km, N' | Ücret:', @Ucret);
END
GO

-- Rota ilerlet
IF OBJECT_ID(N'dbo.sp_RotaIlerle', N'P') IS NOT NULL DROP PROCEDURE dbo.sp_RotaIlerle;
GO
CREATE PROCEDURE dbo.sp_RotaIlerle
    @TakipNo NVARCHAR(30)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @PaketID INT, @DurumID INT, @VarisSubeID INT;

    SELECT TOP 1
        @PaketID = PaketID,
        @DurumID = DurumID,
        @VarisSubeID = VarisSubeID
    FROM dbo.Paket
    WHERE TakipNo = @TakipNo;

    IF @PaketID IS NULL
    BEGIN
        PRINT N'Paket bulunamadı.';
        RETURN;
    END

    DECLARE @Olu INT = (SELECT DurumID FROM dbo.PaketDurum WHERE DurumAdi = N'Oluşturuldu');
    DECLARE @Dag INT = (SELECT DurumID FROM dbo.PaketDurum WHERE DurumAdi = N'Dağıtımda');
    DECLARE @Varis INT = (SELECT DurumID FROM dbo.PaketDurum WHERE DurumAdi = N'Varış Şehrinde');
    DECLARE @Teslim INT = (SELECT DurumID FROM dbo.PaketDurum WHERE DurumAdi = N'Teslim Edildi');

    IF @DurumID = @Olu
    BEGIN
        UPDATE dbo.Paket SET DurumID = @Dag WHERE PaketID = @PaketID;
        INSERT INTO dbo.PaketHareket(PaketID, Aciklama) VALUES(@PaketID, N'Kurye dağıtıma çıktı');
        PRINT N'Durum -> Dağıtımda';
        RETURN;
    END

    IF @DurumID = @Dag
    BEGIN
        IF @VarisSubeID IS NOT NULL
        BEGIN
            UPDATE dbo.Paket
            SET AnlikSubeID = @VarisSubeID, DurumID = @Varis
            WHERE PaketID = @PaketID;

            INSERT INTO dbo.PaketHareket(PaketID, Aciklama) VALUES(@PaketID, N'Paket varış şehrine ulaştı');
            PRINT N'Durum -> Varış Şehrinde';
        END
        RETURN;
    END

    IF @DurumID = @Varis
    BEGIN
        UPDATE dbo.Paket SET DurumID = @Teslim WHERE PaketID = @PaketID;
        INSERT INTO dbo.PaketHareket(PaketID, Aciklama) VALUES(@PaketID, N'Paket alıcıya teslim edildi');
        PRINT N'Durum -> Teslim Edildi';
        RETURN;
    END

    IF @DurumID = @Teslim
        PRINT N'Paket zaten teslim edilmiş.';
END
GO

------------------------------------------------------------
-- 7.1) DEMO VERİ ÜRET (DÜZELTİLDİ)
------------------------------------------------------------
IF OBJECT_ID(N'dbo.sp_DemoVeriUret', N'P') IS NOT NULL DROP PROCEDURE dbo.sp_DemoVeriUret;
GO
CREATE PROCEDURE dbo.sp_DemoVeriUret
    @Adet INT = 20
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @i INT = 1;

    WHILE @i <= @Adet
    BEGIN
        DECLARE @Cikis NVARCHAR(50);
        DECLARE @Varis NVARCHAR(50);
        DECLARE @TakipNo NVARCHAR(30);

        SELECT TOP 1 @Cikis = SehirAdi FROM dbo.TurkiyeSehir ORDER BY NEWID();
        SELECT TOP 1 @Varis = SehirAdi FROM dbo.TurkiyeSehir WHERE SehirAdi <> @Cikis ORDER BY NEWID();

        SET @TakipNo = N'TRK-' + CONVERT(NVARCHAR(4), YEAR(GETDATE())) + N'-' +
                       RIGHT(N'000000' + CONVERT(NVARCHAR(6), ABS(CHECKSUM(NEWID())) % 999999), 6);

        IF NOT EXISTS (SELECT 1 FROM dbo.Paket WHERE TakipNo = @TakipNo)
        BEGIN
            DECLARE @GonderenDemo NVARCHAR(100) = N'Demo Gönderen ' + CONVERT(NVARCHAR(10), @i);
            DECLARE @AliciDemo    NVARCHAR(100) = N'Demo Alıcı '    + CONVERT(NVARCHAR(10), @i);

            EXEC dbo.sp_PaketOlusturTR
                @TakipNo    = @TakipNo,
                @Gonderen   = @GonderenDemo,
                @Alici      = @AliciDemo,
                @CikisSehir = @Cikis,
                @VarisSehir = @Varis;
        END

        SET @i = @i + 1;
    END

    PRINT N'Demo veri üretildi. Adet: ' + CONVERT(NVARCHAR(10), @Adet);
END
GO

------------------------------------------------------------
-- 8) TEK ANA MENÜ
------------------------------------------------------------
PRINT '=================================================';
PRINT '     KURYE TESLİM SİSTEMİ - TEK ANA MENÜ';
PRINT '=================================================';
PRINT '1) Şehir listesi (81 il)';
PRINT '2) Yeni Paket Oluştur (Çıkış/Varış şehir seç)';
PRINT '3) Paket Konum / Takip Ekranı (TakipNo)';
PRINT '4) Rota İlerlet (1 adım) (TakipNo)';
PRINT '5) Amasya Otomatik Kurye Atama';
PRINT '6) Şube Gelir Raporu';
PRINT '7) Son Paketler (Özet)';
PRINT '8) Demo Veri Üret (N adet) + Özet Çıktı';
PRINT '-------------------------------------------------';
PRINT 'Kullanım: Aşağıdaki @AnaSecim değerini değiştirip F5 bas.';
PRINT '=================================================';

DECLARE @AnaSecim INT = 8;  -- 1..8

-- Paket oluşturma parametreleri
DECLARE @YeniTakip NVARCHAR(30) = N'TRK-2026-000011';
DECLARE @CikisSehir NVARCHAR(50) = N'İstanbul';
DECLARE @VarisSehir NVARCHAR(50) = N'Gaziantep';
DECLARE @Gonderen NVARCHAR(100) = N'Hasan Gürer';
DECLARE @Alici NVARCHAR(100) = N'Büşra Yılmaz';

-- Takip ekranı
DECLARE @Takip NVARCHAR(30) = @YeniTakip;

-- Demo
DECLARE @DemoAdet INT = 20;

IF @AnaSecim = 1
BEGIN
    SELECT PlakaKodu, SehirAdi FROM dbo.TurkiyeSehir ORDER BY PlakaKodu;
END
ELSE IF @AnaSecim = 2
BEGIN
    EXEC dbo.sp_PaketOlusturTR
        @TakipNo = @YeniTakip,
        @Gonderen = @Gonderen,
        @Alici = @Alici,
        @CikisSehir = @CikisSehir,
        @VarisSehir = @VarisSehir;

    SELECT * FROM dbo.vw_PaketKonum WHERE TakipNo = @YeniTakip;
END
ELSE IF @AnaSecim = 3
BEGIN
    SELECT * FROM dbo.vw_PaketKonum WHERE TakipNo = @Takip;

    SELECT p.TakipNo, ph.TarihSaat, ph.Aciklama
    FROM dbo.PaketHareket ph
    JOIN dbo.Paket p ON p.PaketID = ph.PaketID
    WHERE p.TakipNo = @Takip
    ORDER BY ph.TarihSaat;
END
ELSE IF @AnaSecim = 4
BEGIN
    EXEC dbo.sp_RotaIlerle @TakipNo = @Takip;
    SELECT * FROM dbo.vw_PaketKonum WHERE TakipNo = @Takip;
END
ELSE IF @AnaSecim = 5
BEGIN
    EXEC dbo.sp_AmasyaOtomatikAtama;
    SELECT TOP 50 * FROM dbo.vw_PaketOzet WHERE CikisSehir = N'Amasya' ORDER BY AtamaTarihi DESC;
END
ELSE IF @AnaSecim = 6
BEGIN
    SELECT sCikis.Sehir AS Sehir, COUNT(*) AS PaketSayisi, SUM(p.Ucret) AS ToplamGelir
    FROM dbo.Paket p
    JOIN dbo.Sube sCikis ON sCikis.SubeID = p.CikisSubeID
    GROUP BY sCikis.Sehir
    ORDER BY ToplamGelir DESC;
END
ELSE IF @AnaSecim = 7
BEGIN
    SELECT TOP 50 * FROM dbo.vw_PaketOzet ORDER BY OlusturmaTarihi DESC;
END
ELSE IF @AnaSecim = 8
BEGIN
    EXEC dbo.sp_DemoVeriUret @Adet = @DemoAdet;

    -- Demo sonrası hazır çıktılar:
    SELECT TOP 50 * FROM dbo.vw_PaketOzet ORDER BY OlusturmaTarihi DESC;
    SELECT TOP 20 * FROM dbo.vw_PaketKonum ORDER BY OlusturmaTarihi DESC;
END
ELSE
BEGIN
    PRINT N'Geçersiz seçim! @AnaSecim = 1..8 olmalı.';
END
GO
