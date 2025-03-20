window.nkJSObject =
{
    objectMap: [null],
    emptySlots: [],
    RegisterObject: function(obj)
    {
        if (obj == null)
            return -1;
        if ('nkUid' in obj)
            throw "object already registered";
            
        // debug check
        //if (nkJSObject.objectMap.indexOf(obj) != -1)
        //    throw "object already registered";

        if (nkJSObject.emptySlots.length == 0)
        {
            nkJSObject.objectMap.push(obj);
            var uid = nkJSObject.objectMap.lastIndexOf(obj);
            obj.nkUid = uid;
            return uid;
        }
        else
        {
            var uid = nkJSObject.emptySlots.pop();

            if (nkJSObject.objectMap[uid] !== undefined)
                throw "slot allready used";

            nkJSObject.objectMap[uid] = obj;
            obj.nkUid = uid;
            return uid;
        }
    },
    GetObject: function(uid)
    {
        return nkJSObject.objectMap[uid];
    },
    GetUid: function(obj)
    {
        if (obj !== null)
        {
            if ('nkUid' in obj)
                return obj.nkUid;
        }

        return -1;
    },
    DisposeObject: function(uid)
    {
        var obj = nkJSObject.objectMap[uid];   
                
        if (obj === undefined)
            throw "obj is undefined";
        if (obj.nkUid !== uid)
            throw "invalid nkUid";

        delete obj.nkUid;
        delete nkJSObject.objectMap[uid];
        nkJSObject.emptySlots.push(uid);
    },
    GetWindow: function()
    {
        return nkJSObject.RegisterObject(window);
    },

    ReadString: function(d)
    {
        const pt = Module.HEAP32[(d)>>2];
        var str = BINDING.conv_string(pt);
        return str;
    },
}

window.nkJSArray =
{
    GetLength: function (uid, d)
    {
        var ar = nkJSObject.GetObject(uid);
        return ar.length;
    },
    GetItem: function (uid, d)
    {
        var ar = nkJSObject.GetObject(uid);
        var id = Module.HEAP32[(d + 0 >> 2)];

        var it = ar[id];
        var uid = nkJSObject.GetUid(it);
        if (uid !== -1)
            return uid;

        return nkJSObject.RegisterObject(it);
    },
};

window.nkPromise =
{
    GetValueBoolean: function (uid)
    {
        var pr = nkJSObject.GetObject(uid);
        return pr.AsyncValue;
    },
    GetValueJSObject: function (uid)
    {
        var pr = nkJSObject.GetObject(uid);

        var ob = pr.AsyncValue;
        var uid = nkJSObject.GetUid(ob);
        if (uid !== -1)
            return uid;

        return nkJSObject.RegisterObject(ob);
    },
    GetErrorType: function (uid)
    {
        var pr = nkJSObject.GetObject(uid);

        if (pr.Error instanceof DOMException)
        {
            switch (pr.Error.name)
            {
                case "InvalidStateError":
                    return 11;
                case "NotSupportedError":
                    return 12;
                case "SecurityError":
                    return 13;

                default:
                    return 10;
            }
        }
        else if (pr.Error instanceof Error)
        {
            return 2;
        }
        else if (typeof pr.Error === "string")
        {
            return 1;
        }
        else
        {
            return 0;
        }
    },
    GetErrorMessage: function (uid)
    {
        var pr = nkJSObject.GetObject(uid);

        if (pr.Error instanceof DOMException)
        {
            var mg = pr.Error.message;
            return BINDING.js_to_mono_obj(mg);
        }
        else if (pr.Error instanceof Error)
        {
            var mg = pr.Error.message;
            return BINDING.js_to_mono_obj(mg);
        }
        else if (typeof pr.Error === "string")
        {
            var mg = pr.Error;
            return BINDING.js_to_mono_obj(mg);
        }
        else
        {
            var mg = "Unknown Error";
            return BINDING.js_to_mono_obj(mg);
        }
    },

    RegisterEvents: function (uid)
    {
        var pr = nkJSObject.GetObject(uid);

        pr.then((value) =>
        {
            pr.AsyncValue = value;
            DotNet.invokeMethod('nkast.Wasm.Dom', 'JsPromiseOnCompleted', uid);
        }
        ).catch((error) =>
        {
            pr.Error = error;
            DotNet.invokeMethod('nkast.Wasm.Dom', 'JsPromiseOnError', uid);
        });
    },
};
