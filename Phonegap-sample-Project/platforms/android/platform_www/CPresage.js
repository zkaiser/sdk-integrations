var CPresage = {
	adToServe: function(onAdEvent, onAdNotFound) {
		cordova.exec(onAdEvent, onAdNotFound, 'CPresage', 'adToServe', [{}]);
	}
};