mergeInto(LibraryManager.library, {

  GoToFullScreen: function () {
	window.GoToFullScreen();
  }, 

  HasFullscreen: function()
    {
        var e = document.createElement('canvas');
        if (e['requestFullScreen'] ||
                e['mozRequestFullScreen'] ||
                e['msRequestFullscreen'] ||
                e['webkitRequestFullScreen'])
        {
                return 1;
        }
        return 0;
    },

    SendMailJS: function(serviceID, templateID, etitle, ename, etime, emessage, eemail)
    {
//        window.alert("title: " + UTF8ToString(etitle) 
//  			+ " name: " + UTF8ToString(ename) 
//  			+ " time: " + UTF8ToString(etime) 
//  			+ " message: " + UTF8ToString(emessage)
//  			+ " email: " + UTF8ToString(eemail));

        var templateParams = {
            title: UTF8ToString(etitle),
            name: UTF8ToString(ename),
            time: UTF8ToString(etime),
            message: UTF8ToString(emessage),
            email: UTF8ToString(eemail),
        };

        emailjs.send('service_8gnjq1v', 'template_90sxgx1', templateParams)
        .then(function (response) {
          console.log('SUCCESS!', response.status, response.text);
        }, function (error) {
          console.log('FAILED...', error);
        });

//      window.SendEmailJS(UTF8ToString(serviceID), UTF8ToString(templateID), UTF8ToString(etitle), UTF8ToString(ename), UTF8ToString(etime), UTF8ToString(emessage), UTF8ToString(eemail));
    },

    DownloadFile: function (content, filename) {
        const contentStr = UTF8ToString(content);
        const filenameStr = UTF8ToString(filename);

        const blob = new Blob([contentStr], { type: "text/csv;charset=utf-8;" });
        const url = URL.createObjectURL(blob);

        const a = document.createElement('a');
        a.href = url;
        a.download = filenameStr;
        document.body.appendChild(a); // Append to body to ensure it's clickable
        a.click();
        document.body.removeChild(a); // Clean up
        URL.revokeObjectURL(url); // Revoke the temporary URL
    },
});