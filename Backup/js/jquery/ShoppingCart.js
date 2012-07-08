Type.registerNamespace("Dflying");
Dflying.DraggableProductBehavior = function(element) {
    // 初始化基类
    Dflying.DraggableProductBehavior.initializeBase(this, [element]);
    
    // 按下鼠标键时的事件处理函数
    this._mouseDownHandler = Function.createDelegate(this, this._handleMouseDown);
    
    // 该可拖动对象所表示的产品
    this._product = null;
    
    // 拖动时跟随鼠标的半透明元素
    this._visual = null;
}
Dflying.DraggableProductBehavior.prototype = {
    // IDragSource接口中的方法
    // 取得该可拖动对象的数据类型——"Product"
    get_dragDataType: function() {
        return "Product";
    },
 
    // 取得该可拖动对象的数据
    getDragData: function(context) {
        return this._product;
    },
 
    // 可拖动对象的拖拽模式——拷贝
    get_dragMode: function() {
        return Sys.Preview.UI.DragMode.Copy;
    },
 
    // 拖动开始时的处理方法
    onDragStart: function() {
    },
 
    // 拖动进行时的处理方法
    onDrag: function() {
    },
 
    // 拖动结束时的处理方法
    onDragEnd: function(canceled) {
        if (this._visual)
            this.get_element().parentNode.removeChild(this._visual);
    },
   
    // product属性
    get_product: function(product) {
        return this._product;
    },
    
    set_product: function(product) {
        this._product = product;
    },
    
    // 初始化方法
    initialize: function() {
        $addHandler(this.get_element(), "mousedown", this._mouseDownHandler);
    },
 
    // mousedown事件处理函数
    _handleMouseDown: function(ev) {
        // DragDropManager需要该项设定
        window._event = ev; 
 
        // 设置拖动时跟随鼠标的半透明元素的样式
        this._visual = this.get_element().cloneNode(true);
        this._visual.style.opacity = "0.7";
        this._visual.style.filter = 
            "progid:DXImageTransform.Microsoft.BasicImage(opacity=0.7)";
        this._visual.style.zIndex = 99999;
        this.get_element().parentNode.appendChild(this._visual);
        var location = Sys.UI.DomElement.getLocation(this.get_element());
        Sys.UI.DomElement.setLocation(this._visual, location.x, location.y);
 
        // 告知DragDropManager开始拖放操作
        Sys.Preview.UI.DragDropManager.startDragDrop(this, this._visual, null);
    },
 
    // 析构方法
    dispose: function() {
        if (this._mouseDownHandler)
            $removeHandler(this.get_element(), "mousedown", this._mouseDownHandler);
        this._mouseDownHandler = null;
        
        Dflying.DraggableProductBehavior.callBaseMethod(this, 'dispose');
    }
}


Dflying.DraggableProductBehavior.registerClass(
    "Dflying.DraggableProductBehavior",
    Sys.UI.Behavior, 
    Sys.Preview.UI.IDragSource
);


Dflying.ShoppingCartBehavior = function(element) {
    // 初始化基类
    Dflying.ShoppingCartBehavior.initializeBase(this, [element]);
    
    // 购物车中的产品列表
    this._products = new Object();
}


Dflying.ShoppingCartBehavior.prototype = {
    // IDropTarget接口中的方法
    // 返回购物车元素
    get_dropTargetElement: function() {
        return this.get_element();
    },
 
    // 判断某可拖动元素是否能够投放在该投放目标对象中
    canDrop: function(dragMode, dataType, data) {
        return (dataType == "Product" && data);
    },
 
    // 将某可拖动元素投放在该投放目标对象中
    drop : function(dragMode, dataType, data) {
        if (dataType == "Product" && data) {
            // 购物车中尚无本产品，设置数量为1
            if (this._products[data.Id] == null) {
                this._products[data.Id] = {Product: data, Quantity: 1};
            }
            // 购物车中已经有本产品，将其数量加1
            else {
                this._products[data.Id].Quantity++;
            }
            
            // 刷新购物车的UI
            this._refreshShoppingCart();
            
            // 将购物车背景颜色设置回白色
            this.get_element().style.backgroundColor = "#fff";
        }
    },
 
    // 某可拖动元素位于该投放目标对象中时的处理方法
    onDragEnterTarget : function(dragMode, dataType, data) {
        if (dataType == "Product" && data) {
            // 设置购物车的背景颜色为灰色
            this.get_element().style.backgroundColor = "#E0E0E0";
        }
    },
   
    // 某可拖动元素离开该投放目标对象时的处理方法
    onDragLeaveTarget : function(dragMode, dataType, data) {
        if (dataType == "Product" && data) {
            // 将购物车背景颜色设置回白色
            this.get_element().style.backgroundColor = "#fff";
        }
    },
 
    // 某可拖动元素在该投放目标对象中拖动时的处理方法
    onDragInTarget : function(dragMode, dataType, data) {
    },
   
    // 根据当前购物车中的产品列表刷新购物车的UI
    _refreshShoppingCart: function() {
        var cartBuilder = new Sys.StringBuilder();
        for (var id in this._products) {
            cartBuilder.append("<div>");
            cartBuilder.append(this._products[id].Product.Name);
            cartBuilder.append(" * ");
            cartBuilder.append(this._products[id].Quantity);
            cartBuilder.append("</div>");
        }
        
        this.get_element().innerHTML = cartBuilder.toString();
    },
    
    // 返回表示当前购物车中的产品Id以及数量的对象
    getProductsToBeOrdered: function() {
        var productsToBeOrdered = new Object();
        
        for (var id in this._products) {
            productsToBeOrdered[id] = this._products[id].Quantity;
        }
        
        return productsToBeOrdered;
    },
    
    // 初始化方法
    initialize: function() {
        // 初始化基类
        Dflying.ShoppingCartBehavior.callBaseMethod(this, "initialize");
        
        // 在DragDropManager中注册该投放目标对象
        Sys.Preview.UI.DragDropManager.registerDropTarget(this);
    },
   
    // 析构方法
    dispose: function() {
        // 在DragDropManager中取消注册该投放目标对象
        Sys.Preview.UI.DragDropManager.unregisterDropTarget(this);
        
        Dflying.ShoppingCartBehavior.callBaseMethod(this, "dispose");
    }
}


Dflying.ShoppingCartBehavior.registerClass("Dflying.ShoppingCartBehavior",
    Sys.UI.Behavior, Sys.Preview.UI.IDropTarget);


if(typeof(Sys)!=='undefined')Sys.Application.notifyScriptLoaded();


function pageLoad(sender, args) {
    // 调用Web Service得到商店中的产品集合
    ShoppingService.GetProducts(onProductsGot);
    
    // 为购物车添加ShoppingCartBehavior行为
    $create(
        Dflying.ShoppingCartBehavior, 
        {"name": "myShoppingCartBehavior"},
        null, 
        null, 
        $get("shoppingCart")
    );
}


function onProductsGot(result) {
    // 获取显示各个产品的容器
    var productContainer = $get("productContainer");
    
    // 遍历服务器端返回的产品集合
    for (var index = 0; index < result.length; ++ index) {
        // 当前产品
        var thisProduct = result[index];
        
        // 根据该产品信息创建DOM元素，并添加到产品容器中
        var productElem = document.createElement("div");
        productElem.innerHTML = thisProduct.Name + " - RMB: "
            + thisProduct.Price;
        productContainer.appendChild(productElem);
        
        // 为该产品添加DraggableProductBehavior行为
        $create(
            Dflying.DraggableProductBehavior, 
            {"product": thisProduct}, // 设置product属性
            null, 
            null, 
            productElem
        );
    }
}


function btnOrder_onclick() {
    // 得到购物车所附加的ShoppingCartBehavior行为
    var shoppingCartBehavior = Sys.UI.Behavior.getBehaviorByName(
        $get("shoppingCart"), 
        "myShoppingCartBehavior"
    );
    
    // 取得当前购物车中各个产品的Id和数量
    var productsToBeOrdered = 
        shoppingCartBehavior.getProductsToBeOrdered();
    
    // 调用Web Service处理订单
    ShoppingService.Order(productsToBeOrdered, onOrdered);
}
在productsToBeOrdered()回调函数中，我们只是简单地使用alert()方法将服务器端的响应提示给用户： 

function onOrdered(result) {
    alert(result);
}











