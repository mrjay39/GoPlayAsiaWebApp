window.chatScriptHelper = {
    addScript: function () {
        if (!document.getElementById('fc_frame')) {
            var script = document.createElement('script');
            script.id = 'chat-script';
            script.src = '//fw-cdn.com/12024965/4557721.js';
            script.setAttribute('chat', 'true');
            document.head.appendChild(script);
        }
    },
    removeScript: function () {
        var existingScript = document.getElementById('fc_frame');
        if (existingScript) {
            existingScript.remove();
        }
    }
};