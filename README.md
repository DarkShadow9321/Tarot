# 星象塔羅 · Tarot Studio

ASP.NET Core MVC + SQL Server 全端練習作品。使用者可輸入問題，抽取一張或三張不重複的塔羅牌，查看正逆位牌義，並回顧歷史紀錄。

![星象塔羅首頁](docs/assets/home.png)

## 專案內容

- `docs/`：面試用靜態展示頁，七張實機截圖、圖片分類與放大檢視，支援手機版面。
- `src/TarotWeb/`：原始 MVC 應用程式，來自本機 TarotWeb 專案。

展示頁僅介紹作品，不連接 SQL Server，也不假裝提供線上 MVC 操作。

## 技術與功能

- .NET 8、C#、ASP.NET Core MVC、Razor Views。
- EF Core 8.0.11 + SQL Server；78 張牌種子資料。
- `TarotDrawService` 使用 Fisher–Yates 與 `RandomNumberGenerator` 洗牌，抽牌不重複並決定正逆位。
- 歷史紀錄依時間排序，取最近 100 筆，支援查看與刪除。
- 牌卡新增、編輯、刪除，以及表單驗證與防偽權杖。
- 資料表為 `TarotCard`、`TarotReading`、`ReadingCard`。同次占卜的位置、牌卡有唯一索引；使用中的牌卡禁止直接刪除。

## 本機啟動 MVC

需求：.NET 8 SDK 與 SQL Server / SQL Server Express LocalDB（Windows）。Visual Studio 2022 須安裝 ASP.NET 與網頁程式開發工作負載。

1. 開啟 `src/TarotWeb/TarotWeb.sln`。
2. 預設連線使用 Windows 整合驗證的 `(localdb)\MSSQLLocalDB`，資料庫為 `TarotWebDemo`。若使用其他 SQL Server，請透過環境變數 `ConnectionStrings__DefaultConnection` 設定連線，勿提交帳號密碼。
3. 在 `src/TarotWeb` 執行 `dotnet restore`，接著執行 `dotnet run --urls http://localhost:5080`。
4. 開啟終端機顯示的本機網址。啟動時透過 `EnsureCreatedAsync` 建立資料庫，再初始化牌卡資料；它不會執行 EF migrations。

MVC 目前限制本機來源，尚未加入帳號與角色授權。公開部署 MVC 前，需完成身分驗證、權限設計與正式資料庫部署策略。

## 預覽展示頁

直接開啟 `docs/index.html`，或在本資料夾執行 `python -m http.server 8080 --directory docs`，再開啟 `http://localhost:8080`。

## GitHub Pages

將此資料夾內容推送到 `DarkShadow9321/Tarot`。在儲存庫的 Settings → Pages，選擇從分支部署，來源設為 `main` 分支的 `/docs`。成功部署後，預期網址為 `https://darkshadow9321.github.io/Tarot/`，實際以 GitHub 顯示的網址為準。

Pages 僅部署靜態展示頁；完整 MVC 後端與 SQL Server 須另外部署。
