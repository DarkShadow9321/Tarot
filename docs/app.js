const screenshots = [
  ['draw', 'reading', '提問與抽牌設定', '輸入問題，選擇單張或三張的抽牌方式。'],
  ['single', 'reading', '單張牌 · 當下的指引', '呈現抽牌結果、正逆位與對應牌義。'],
  ['triple', 'reading', '三張牌 · 過去、現在、未來', '以三個位置組織結果，同時查看各張牌的解讀。'],
  ['history', 'data', '歷史紀錄 · 回顧每次提問', '依時間排列已儲存的占卜，提供查看與刪除入口。'],
  ['cards', 'data', '牌卡管理 · 維護內容', '瀏覽 78 張牌，並提供新增、編輯與刪除功能。'],
  ['history-empty', 'data', '空白狀態 · 第一次探索', '沒有紀錄時，提供清楚的開始占卜入口。']
];
const gallery = document.querySelector('#gallery');
for (const [file, category, title, description] of screenshots) {
  const figure = document.createElement('figure');
  figure.dataset.category = category;
  const button = document.createElement('button');
  button.className = 'image-button';
  button.dataset.full = `assets/${file}.png`;
  button.dataset.caption = title;
  button.setAttribute('aria-label', `放大：${title}`);
  const img = document.createElement('img');
  img.src = button.dataset.full;
  img.alt = `${title}。${description}`;
  img.loading = 'lazy';
  img.width = 1904; img.height = 940;
  button.append(img);
  const caption = document.createElement('figcaption');
  const heading = document.createElement('h4'); heading.textContent = title;
  const text = document.createElement('p'); text.textContent = description;
  caption.append(heading, text); figure.append(button, caption); gallery.append(figure);
}
document.querySelectorAll('[data-filter]').forEach(button => {
  button.addEventListener('click', () => {
    document.querySelectorAll('[data-filter]').forEach(item => item.setAttribute('aria-pressed', String(item === button)));
    gallery.querySelectorAll('figure').forEach(item => { item.hidden = button.dataset.filter !== 'all' && item.dataset.category !== button.dataset.filter; });
  });
});
const dialog = document.querySelector('#lightbox');
document.querySelectorAll('[data-full]').forEach(button => {
  button.addEventListener('click', () => {
    const img = document.querySelector('#lightbox-image');
    img.src = button.dataset.full; img.alt = button.dataset.caption;
    document.querySelector('#lightbox-caption').textContent = button.dataset.caption;
    dialog.showModal();
  });
});
document.querySelector('#close-dialog').addEventListener('click', () => dialog.close());
dialog.addEventListener('click', event => { if (event.target === dialog) { const rect = dialog.getBoundingClientRect(); if (event.clientX < rect.left || event.clientX > rect.right || event.clientY < rect.top || event.clientY > rect.bottom) dialog.close(); } });
