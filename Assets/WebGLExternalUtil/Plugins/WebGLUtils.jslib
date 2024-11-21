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
    }

});