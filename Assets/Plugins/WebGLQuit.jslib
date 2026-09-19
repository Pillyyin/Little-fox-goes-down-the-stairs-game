mergeInto(LibraryManager.library, {

    // 供 C# 調用的關閉網頁函數
    QuitWebGL: function () {
        // 嘗試關閉當前分頁
        window.close();

        // 備用機制：如果 window.close() 被瀏覽器阻擋（非 JS 開啟的分頁），則導向空白頁或結束頁
        if (!window.closed) {
            // 可替換為你的遊戲結束頁面網址，或跳轉到空白頁
            window.location.href = "about:blank"; 
        }
    }
});