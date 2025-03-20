window.nkDocument =
{
    GetTitle: function (uid)
    {
        var dc = nkJSObject.GetObject(uid);
        return BINDING.js_to_mono_obj(dc.title);
    },
    SetTitle: function (uid, d)
    {
        var dc = nkJSObject.GetObject(uid);
        var tl = nkJSObject.ReadString(d+ 0);
        dc.title = tl;
    },
    GetElementById: function (uid, d)
    {
        var dc = nkJSObject.GetObject(uid);
        var id = nkJSObject.ReadString(d+ 0);
        var el = dc.getElementById(id);
        return nkJSObject.RegisterObject(el);
    },
    HasFocus: function (uid)
    {
        var dc = nkJSObject.GetObject(uid);
        return dc.hasFocus();
    }
};

window.nkElement =
{
    GetClientWidth: function(uid)
    {
        var e = nkJSObject.GetObject(uid);
        return e.clientWidth;
    },
    GetClientHeight: function (uid)
    {
        var e = nkJSObject.GetObject(uid);
        return e.clientHeight;
    },
};
