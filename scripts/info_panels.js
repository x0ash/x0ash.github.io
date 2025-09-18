function outer_redirect(self) {
    /*
        By default due to the way I'm loading HTML files, the object window will be redirected when clicking on a link object. 
        I want to have the target URL display like it would with an <a> tag but with the main window redirecting instead.
        As a result, I use this function to redirect the main page to the target URL.
    */
    var link = self.href;   // Firstly I want to obtain the target link
    window.location.href = window.location.href;    // I force-set the object's location to the current one, otherwise it would redirect to the clicked link.
                                                    // This avoids a CSO error, which would be displayed to the user.
    window.parent.window.location.href = link;      // I set the parent window's location to the target link.
}