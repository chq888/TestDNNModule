myApp.controller('productctrl', ['$scope', 'serviceAPI', function ($scope, serviceAPI) {

    $scope.vm = {};
    $scope.NoSelected = { Name: "Select an item", Id: -1 };

    $scope.search = function () {
        var data = { Code: $scope.vm.Code, CategoryId: $scope.vm.selectedCategory.Id }
        serviceAPI.post("desktopmodules/services/API/ProductApi/Search", data).then(function successCallback(res) {
            $scope.vm.list = res.data;
        }, function errorCallback(err) {
        });
    }

    $scope.del = function (id) {
        var r = confirm("Are you sure you want to delete it?");
        if (r == true) {
            serviceAPI.del("desktopmodules/services/API/ProductApi/Delete/" + id).then(function successCallback(res) {
                window.location.href = $scope.tabPath;
            }, function errorCallback(err) {
            });
        }
    }

    function init() {
        serviceAPI.get("desktopmodules/services/API/ProductApi/GET").then(function successCallback(res) {
            $scope.vm.list = res.data;
        }, function errorCallback(err) {
        });

        serviceAPI.get("desktopmodules/services/API/CategoryApi/GET")
            .then(function successCallback(res) {
                $scope.vm.Categories = res.data;
                $scope.vm.Categories.splice(0, 0, $scope.NoSelected);
                $scope.vm.selectedCategory = $scope.NoSelected;
            },
            function errorCallback(err) {
            });
    }

    $scope.onInit = function (val, tabPath) {
        $scope.baseUrl = val;
        $scope.tabPath = tabPath.replace("//", "");

        init();
    }

}]);


myApp.controller('productformctrl', ['$scope', 'serviceAPI', '$timeout', function ($scope, serviceAPI, $timeout) {

    $scope.isLoaded = false;
    $scope.vm = {};
    $scope.vm.SelectedOldCategories = [];
    $scope.cancel = function () {
    }
    $scope.save = function () {
        var data = {};
        data.Product = $scope.vm.Product;
        data.Colors = $scope.vm.Colors;
        var noChangeList = _.intersection($scope.vm.SelectedOldCategories, $scope.vm.SelectedCategories);
        var removedList = _.difference($scope.vm.SelectedOldCategories, $scope.vm.SelectedCategories);
        var addedList = _.difference($scope.vm.SelectedCategories, $scope.vm.SelectedOldCategories);
        //TODO check noChangeList, now it's ok
        data.SelectedCategories = [];

        _.forEach(noChangeList, function (value) {
            removedList = _.remove(removedList, function (n) {
                return value.Id == n.Id;
            });
            addedList = _.remove(addedList, function (n) {
                return value.Id == n.Id;
            });
        });

        _.forEach(removedList, function (value) {
            value.IsDirty = true;
            data.SelectedCategories.push(value);
        });

        _.forEach(addedList, function (value) {
            value.IsDirty = null;
            data.SelectedCategories.push(value);
        });

        serviceAPI.post("http://" + $scope.baseUrl + "/desktopmodules/services/API/ProductApi/POST", data).then(function successCallback(res) {
            window.location.href = "http://" + $scope.baseUrl + "/" + $scope.tabPath;
        }, function errorCallback(err) {
        });
    }

    $scope.addColor = function () {
        $timeout(function () {
            $scope.$apply(function () {
                var list = $scope.vm.Colors;
                list.push({
                    Text: "#44aa77",
                    Description: "",
                    Id: "",
                    IsDirty: null
                });

                $scope.vm.Colors = list;
            });
        });
    }

    function init() {
        if ($scope.id > 0) {
            serviceAPI.get("http://" + $scope.baseUrl + "/desktopmodules/services/API/ProductApi/GET/" + $scope.id)
              .then(function successCallback(res) {
                  $scope.vm = res.data;
                  $scope.vm.SelectedOldCategories = _.cloneDeep($scope.vm.SelectedCategories);
                  ;
              },
                function errorCallback(err) {
                });
        } else {
            serviceAPI.get("http://" + $scope.baseUrl + "/desktopmodules/services/API/CategoryApi/GET")
            .then(function successCallback(res) {
                $scope.vm.Categories = res.data;
            },
            function errorCallback(err) {
            });

            $scope.vm.Product = {};
            $scope.vm.Colors = [];
            $scope.vm.Product.ModuleId = $scope.moduleId;
        }
    }

    $scope.onInit = function (url, tabPath, id, moduleId) {
        $scope.baseUrl = url;
        $scope.tabPath = tabPath.replace("//", "");
        $scope.id = id;
        $scope.moduleId = moduleId;
        init();
    }

}]);
