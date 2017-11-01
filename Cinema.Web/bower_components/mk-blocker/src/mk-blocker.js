var mkBlock = angular.module('mkBlock',[]);

var mkBlock_tpl1 = '<div data-ng-show="isBlocked" class="mk-overlay-wrap">' + 
						'<div class="mk-overlay">' + 
							'<p>loading....</p>' + 
							'<span class="mk-spin" data-ng-style="customStyles"></span>' + 
						'</div>' + 
					'</div>';

mkBlock.service('mkBlocker', function() {
	this.isBlocked = false;
	this.spinColor = null;
	this.blockUI = function(){
		this.isBlocked = true;
		return this.isBlocked;
	};
	this.unblockUI = function(){
		this.isBlocked = false;
		return this.isBlocked;
	};
	this.toggleUI = function(){
		this.isBlocked = !this.isBlocked;
		return this.isBlocked;
	};
});

mkBlock.directive('mkBlockerElem', ['mkBlocker', function(mkBlocker){
	return{
		restrict: 'EA',
		replace: true,
		scope: true,
		template: mkBlock_tpl1,
		link: function(scope, element, attrs){
			scope.$watch(function(){
				return mkBlocker.spinColor;
			}, function(){
				scope.spinColor = mkBlocker.spinColor;
				if(scope.spinColor){
					scope.customStyles = {
						"borderColor": scope.spinColor,
						"borderTopColor": "transparent"
					}
				}
			});
			scope.$watch(function(){
				return mkBlocker.isBlocked;
			}, function(){
				scope.isBlocked = mkBlocker.isBlocked;
			});
		}
	};
}]);

mkBlock.directive('body', ['$compile', function ($compile) {
	return {
		restrict: 'E',
		terminal: true,
		compile: function(element, attrs){
			return{
				pre: function(scope, element, attrs){},
				post: function(scope, element, attrs){
					element.append(angular.element("<mk-blocker-elem></mk-blocker-elem>"));
					$compile(element.contents())(scope);
				}
			}
		}
	};
}]);	