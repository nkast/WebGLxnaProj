window.nkMedia =
{
    GetCurrentSrc: function (uid)
    {
        var me = nkJSObject.GetObject(uid);
        return BINDING.js_to_mono_obj(me.currentSrc);
    },

    GetCurrentTime: function (uid)
    {
        var me = nkJSObject.GetObject(uid);
        return me.currentTime * 1000;
    },
    
    GetEnded: function (uid, d)
    {
        var me = nkJSObject.GetObject(uid);
        return me.ended;
    },
    
    GetPaused: function (uid, d)
    {
        var me = nkJSObject.GetObject(uid);
        return me.paused;
    },

    GetSrc: function (uid)
    {
        var me = nkJSObject.GetObject(uid);
        return BINDING.js_to_mono_obj(me.src);
    },
    SetSrc: function (uid, d)
    {
        var me = nkJSObject.GetObject(uid);
        var sr = nkJSObject.ReadString(d+ 0);
        me.src = sr;
    },
    
    GetMuted: function (uid, d)
    {
        var me = nkJSObject.GetObject(uid);
        return me.muted;
    },
    SetMuted: function (uid, d)
    {
        var me = nkJSObject.GetObject(uid);
        var mu = Module.HEAP32[(d+ 0)>>2];
        me.muted = mu !== 0;
    },
    
    GetLoop: function (uid, d)
    {
        var me = nkJSObject.GetObject(uid);
        return me.loop;
    },
    SetLoop: function (uid, d)
    {
        var me = nkJSObject.GetObject(uid);
        var lo = Module.HEAP32[(d+ 0)>>2];
        me.loop = lo !== 0;
    },
    
    GetVolume: function (uid, d)
    {
        var me = nkJSObject.GetObject(uid);
        return me.volume;
    },
    SetVolume: function (uid, d)
    {
        var me = nkJSObject.GetObject(uid);
        var vo = Module.HEAPF32[(d+ 0)>>2];
        me.volume = vo;
    },
    
    Load: function (uid, d)
    {
        var me = nkJSObject.GetObject(uid);
        me.load();
    },
    Play: function (uid, d)
    {
        var me = nkJSObject.GetObject(uid);
        var pm = me.play();
    },
    Pause: function (uid, d)
    {
        var me = nkJSObject.GetObject(uid);
         me.pause();
        return;
    },
    
    RegisterEvents: function (uid)
    {
        var bs = nkJSObject.GetObject(uid);

        bs.onended = function(event)
        {
            DotNet.invokeMethod('nkast.Wasm.Dom', 'JsMediaOnEnded', uid);
        };
        bs.onplaying = function(event)
        {
            DotNet.invokeMethod('nkast.Wasm.Dom', 'JsMediaOnPlaying', uid);
        };
        bs.ontimeupdate = function(event)
        {
            DotNet.invokeMethod('nkast.Wasm.Dom', 'JsMediaOnOnTimeUpdate', uid);
        };
    },
    UnregisterEvents: function (uid)
    {
        var me = nkJSObject.GetObject(uid);
        me.onended = null;
        me.onplaying = null;
        me.ontimeupdate = null;
    }
};

window.nkAudio =
{
    Create: function (uid)
    {
        var au = new Audio();
        return nkJSObject.RegisterObject(au);
    }
};

window.nkVideo =
{
    Create: function (uid)
    {
        var vi = document.createElement("video");
        return nkJSObject.RegisterObject(vi);
    }
};

