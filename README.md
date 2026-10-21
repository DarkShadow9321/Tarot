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

## 實際操作畫面

以下為本機 MVC 系統的實際操作結果，可直接在此查看，無須安裝環境或啟動伺服器。首頁畫面見本文開頭，其餘畫面依使用流程介紹如下。

### 1. 提問與抽牌設定

使用者輸入想詢問的問題，再選擇一張牌或三張牌。一張牌提供當下提醒；三張牌則以過去、現在與未來組織結果。送出表單後，由 C# 後端執行抽牌並決定正逆位。

![提問與抽牌設定：問題輸入欄、一張或三張牌選項及抽牌按鈕](docs/assets/draw.png)

### 2. 單張牌結果

結果頁呈現本次問題、占卜編號、時間、抽出的牌卡、正逆位與對應牌義。截圖展示「審判」正位的單張牌結果。

![單張牌結果：審判正位與牌義說明](docs/assets/single.png)

### 3. 三張牌結果

三張牌分別對應過去、現在與未來，每張牌獨立顯示牌名、正逆位與牌義。截圖中依序為審判正位、寶劍九正位與錢幣騎士逆位。

![三張牌結果：過去、現在與未來的牌卡及個別牌義](docs/assets/triple.png)

### 4. 歷史紀錄

完成占卜後，問題與抽牌結果會儲存至 SQL Server。歷史頁依時間顯示最近 100 筆紀錄，每筆列出占卜編號、時間、抽牌張數與問題，並提供查看及刪除操作。

![歷史紀錄：已儲存的單張與三張占卜，以及查看與刪除按鈕](docs/assets/history.png)

### 5. 尚無紀錄的畫面

當資料庫尚未有占卜紀錄時，頁面顯示「還沒有占卜紀錄」，並提供「開始第一次占卜」入口，引導使用者進入抽牌流程。

![歷史紀錄空白狀態：提示訊息與開始第一次占卜按鈕](docs/assets/history-empty.png)

### 6. 塔羅牌資料管理

管理頁列出牌卡序號、中英文牌名與牌組，提供新增、編輯與刪除入口。系統包含 78 張牌的種子資料；已有占卜紀錄引用的牌卡會限制刪除，以保留資料關聯的完整性。目前管理功能為本機練習用途，尚未加入帳號驗證與角色授權。

![塔羅牌資料管理：牌卡列表、新增牌卡及各筆編輯與刪除按鈕](docs/assets/cards.png)

## GitHub Pages

將此資料夾內容推送到 `DarkShadow9321/Tarot`。在儲存庫的 Settings → Pages，選擇從分支部署，來源設為 `main` 分支的 `/docs`。成功部署後，預期網址為 `https://darkshadow9321.github.io/Tarot/`，實際以 GitHub 顯示的網址為準。

Pages 僅部署靜態展示頁；完整 MVC 後端與 SQL Server 須另外部署。
