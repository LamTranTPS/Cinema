# mk-blocker
Angular Loader, Blocker, Unblocker

---

##### Installation

```terminal

	bower install mk-blocker
    
```

#### Dependencies

* Angular JS

##### Add below files to your application

* CSS
	* `mk-blocker.css` *(Uncompressed file)*
	* `mk-blocker.min.css` *(Compressed file)*
	
* JS
	* `mk-blocker.css` *(Uncompressed file)*
	* `mk-blocker.min.css` *(Compressed file)*

Note: All files are avaible in `./src` folder.

##### Inject dependency into your applicaiton

```javascript

	angular.module('yourModule', ['mkBlock']);
    
```

##### Usage

```javascript

	angular.module('yourModule').controller('yourCtrl', ['mkBlocker', function(mkBlocker){
		mkBlocker.blockUI(); // to block the UI
		mkBlocker.unblockUI(); // to unblock the UI
		mkBlocker.toggleUI(); // to toggle the UI blocking
	}]);
    
```
