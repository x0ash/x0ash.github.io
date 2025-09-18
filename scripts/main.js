// =-= Constants =-=

const info_obj = document.getElementById("info_obj");

// =-= Functions =-=

function load_page(filename) {
	// In: name of HTML file (excluding extension)
	// Expected Output: displays the contents of that file w/ automatic page formatting
	
	// Firstly we change which button is 'clicked'
	var clicked = document.getElementsByClassName("clicked")[0]; 		// Only one element should have .clicked applied to it at a time.
	if (clicked != null) {
		clicked.classList.remove("clicked");
	}

	document.getElementById(`btn_${filename}`).classList.add("clicked");

	var path = `/info_panel/${filename}.html`
	console.log(`Loading ${path}`);
	info_obj.style.opacity = 0; 		// Originally this blink was to hide the page loading without styling - I kept it as a stylistic choice.
	setTimeout(function() { 
		info_obj.data = path; 
	},100); 	// This value is synchronised with the transition-duration of info_div (set in stylesheet.css)
}

function resolve_hash(self) {
	// In: self (this) element
	// Expected Output: URI hash changes

	// This function alters the hash in the URI to match the target hash of a button given by its id. The event listener then calls parse_hash().
	hash = self.id.substring(4);
	window.location.hash = hash;
}

function parse_hash() {
	// In: None
	// Expected Output: loads page found in hash
	var hash = window.location.hash.substring(1);
	load_page(hash);
}

// =-= Main operations =-=

// Deal with hash loading (convenience)
if (window.location.hash) {
	parse_hash();
}

info_obj.addEventListener("load",function() {
	// object/content/head/
	info_obj.contentDocument.children[0].children[0].innerHTML = '<meta charset="utf8"><link rel="stylesheet" href="/stylesheet.css">';

	// object/content/body/
	info_obj.contentDocument.children[0].children[1].style.backgroundColor = 'rgba(0,0,0,0)';
	info_obj.style.opacity = 1;
});

// Parses the hash upon a hashchange event (the hash changing in the URI)
window.addEventListener("hashchange",function() {
	parse_hash();
});

// Dynamically sets up main navigation buttons with images and event listeners. Makes the HTML much cleaner and easier to work with.
document.getElementById("button_div").childNodes.forEach(element => {
	if (element.nodeName == "BUTTON") {
		element.style.backgroundImage = `url("/image/thumbs/${element.id.substring(4)}.jpg")`; 		// the id is always btn_[...] so strip first 4 chars
		element.addEventListener("click",function() {
			resolve_hash(this);
		})
	}
});

// If there is no hash in the URI then add one pointing to home (the default object anyway)
if (window.location.hash == "") {
	window.location.hash = "home";
}