window.nkNavigator =
{
    GetUserAgent: function (uid)
    {
        var nv = nkJSObject.GetObject(uid);
        return BINDING.js_to_mono_obj(nv.userAgent);
    },
    GetMaxTouchPoints: function (uid)
    {
        var nv = nkJSObject.GetObject(uid);
        return nv.maxTouchPoints;
    },
    GetGamepads: function(uid)
    {
        var nv = nkJSObject.GetObject(uid);
        
        var gps = nv.getGamepads();
        var uid = nkJSObject.GetUid(gps);
        if (uid !== -1)
            return uid;

        return nkJSObject.RegisterObject(gps);
    },
    Vibrate: function (uid, d)
    {
        var nv = nkJSObject.GetObject(uid);
        var ms = Module.HEAP32[(d+ 0)>>2];

        if ('vibrate' in nv)
        {
            nv.vibrate(ms);
        }
    },
};
