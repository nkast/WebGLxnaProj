window.nkGamepad =
{
    GetVibrationActuator: function(uid)
    {
        var gp = nkJSObject.GetObject(uid);

        if ('vibrationActuator' in gp)
        {
            var ha = gp.vibrationActuator;

            var uid = nkJSObject.GetUid(ha);
            if (uid !== -1)
                return uid;

            return nkJSObject.RegisterObject(ha);
        }
        else 
        return -1;
    },
    GetId: function (uid)
    {
        var gp = nkJSObject.GetObject(uid);
        return BINDING.js_to_mono_obj(gp.id);
    },
    GetIndex: function (uid, d)
    {
        var gp = nkJSObject.GetObject(uid);
        return gp.index;
    },
    GetConnected: function (uid, d)
    {
        var gp = nkJSObject.GetObject(uid);
        return gp.connected;
    },
    GetMapping: function (uid, d)
    {
        var gp = nkJSObject.GetObject(uid);
        return BINDING.js_to_mono_obj(gp.mapping);
    },
    GetTimestamp: function (uid, d)
    {
        var gp = nkJSObject.GetObject(uid);
        return Math.floor(gp.timestamp*1000);
    },
    GetButtons: function (uid, d)
    {
        var gp = nkJSObject.GetObject(uid);
        var ct = Module.HEAP32[(d+ 0)>>2];
        var st = Module.HEAP32[(d+ 4)>>2];
        var pt = Module.HEAP32[(d+ 8)>>2];

        var buttons = gp.buttons;

        if (ct<buttons.length)
            return -buttons.length;
            
        for (var i = 0; i < buttons.length; i++)
        {
            Module.HEAPF32[(pt+ 0 + i*st)>>2] = buttons[i].value;
            Module.HEAPU8[(pt+ 4 + i*st)>>0]  = (buttons[i].pressed) ? 1 : 0;
            Module.HEAPU8[(pt+ 5 + i*st)>>0]  = (buttons[i].touched !== undefined && buttons[i].touched) ? 1 : 0;
        }

        return buttons.length;
    },
    GetAxes: function (uid, d)
    {
        var gp = nkJSObject.GetObject(uid);
        var ct = Module.HEAP32[(d+ 0)>>2];
        var pt = Module.HEAP32[(d+ 4)>>2];

        var axes = gp.axes;
        
        if (ct<axes.length)
            return -axes.length;
            
        for (var i = 0; i < axes.length; i++)
        {
            Module.HEAPF32[(pt+ 0 + i*4)>>2] = axes[i];
        }

        return axes.length;
    }
};

window.nkGamepadHapticActuator =
{
    PlayEffect: function (uid, d)
    {
        var ha = nkJSObject.GetObject(uid);
        var sd = Module.HEAPF32[(d+ 0)>>2];
        var du = Module.HEAPF32[(d+ 4)>>2];
        var sm = Module.HEAPF32[(d+ 8)>>2];
        var wm = Module.HEAPF32[(d+12)>>2];
        var lt = Module.HEAPF32[(d+16)>>2];
        var rt = Module.HEAPF32[(d+20)>>2];

        if ('playEffect' in ha)
        {
            ha.playEffect("dual-rumble",
            {
                startDelay: sd,
                duration: du,
                strongMagnitude: sm,
                weakMagnitude: wm,
                leftTrigger: lt,
                rightTrigger: rt
            });
            return true;
        }
        return false;
    },
    Pulse: function (uid, d)
    {
        var ha = nkJSObject.GetObject(uid);
        var va = Module.HEAPF32[(d+ 0)>>2];
        var du = Module.HEAPF32[(d+ 4)>>2];

        if ('pulse' in ha)
        {
            ha.pulse(va, du);
            return true;
        }
        return false;
    },
    Reset: function (uid, d)
    {
        var ha = nkJSObject.GetObject(uid);

        if ('reset' in ha)
        {
            ha.reset();
            return true;
        }
        return false;
    },
};


