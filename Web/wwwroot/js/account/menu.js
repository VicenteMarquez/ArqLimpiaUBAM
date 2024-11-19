$(document).ready(function () {

    cargarNavPrincipal();
    function cargarNavPrincipal() {
        $.ajax({
            url: obtenBase() + "Home/Menu",
            type: "POST",
            success: function (responseView) {
                $("#NavPrincipal").html(responseView);
                cargarMenus();
            },
            error: function (xhr, status, error) {
                var errorMessage = xhr.status + ': ' + xhr.statusText
                console.log(this);
            },
        });
    }

    function cargarMenus() {
        try {
            $.ajax({
                url: obtenBase() + "Home/CargarMenus",
                type: "POST",
                dataType: "json",
                success: function (responseData) {
                    if (responseData != "") {
                        if (responseData.IdCodigo == 109) {
                            window.location = obtenBase() + "Account/Login";
                        }
                        else {
                            var menuContainer = $("#Menus");
                            menuContainer.empty();
                            $(responseData).each(function () {
                                // botón Menú principal
                                var li = $('<li>', {
                                    class: "nav-item dropdown",
                                });
                                var a = $('<a>', {
                                    class: "nav-link dropdown-toggle",
                                    id: this.NombreMenu.replace(" ", ""),
                                    name: this.NombreMenu.replace(" ", ""),
                                    text: this.NombreMenu
                                });
                                $(a).attr("href", "#");
                                $(a).attr("data-toggle", "dropdown");
                                $(a).attr("aria-haspopup", "true");
                                $(a).attr("aria-expanded", "false");
                                $(li).append(a);
                                // fin botón Menú principal

                                if (this.Links.length > 0 && this.SubMenus.length > 0) {
                                    var ulsubmenu = $("<ul>", {
                                        class: "dropdown-menu"
                                    });

                                    $(this.Links).each(function () {

                                        var link = "";
                                        if (this.Nombre === "_") {
                                            link = $("<div>",
                                                {
                                                    class: "dropdown-divider"
                                                });
                                        }
                                        else {
                                            var link = $("<li>");

                                            linka = $("<a>", {
                                                class: "dropdown-item",
                                                text: this.Nombre
                                            });
                                            linka.attr("href", obtenBase() + this.URL);

                                            link.append(linka);
                                        }
                                        ulsubmenu.append(link);

                                    });

                                    $(this.SubMenus).each(function () {
                                        var submenuli = $("<li>", {
                                            class: "dropdown-submenu"
                                        });

                                        var submenua = $("<a>", {
                                            class: "dropdown-item dropdown-toggle",
                                            text: this.NombreSubMenu
                                        });
                                        submenua.attr("href", "#");
                                        submenuli.append(submenua);

                                        var submenulu = $("<ul>", {
                                            class: "dropdown-menu"
                                        });
                                        submenuli.append(submenulu);

                                        $(this.Links).each(function () {
                                            var submenulisa = $("<li>");
                                            var subMenu = $("<a>", {
                                                class: "dropdown-item",
                                                text: this.Nombre
                                            });
                                            subMenu.attr("href", obtenBase() + this.URL);

                                            submenulisa.append(subMenu);
                                            submenulu.append(submenulisa);
                                        });

                                        ulsubmenu.append(submenuli);
                                    });

                                    $(li).append(ulsubmenu);
                                }
                                else if (this.Links.length > 0) {
                                    var divLink = $("<ul>", {
                                        class: "dropdown-menu"
                                    });

                                    $(this.Links).each(function () {

                                        var link = "";
                                        if (this.Nombre === "_") {
                                            link = $("<div>",
                                                {
                                                    class: "dropdown-divider"
                                                });
                                        }
                                        else {
                                            var link = $("<li>");

                                            linka = $("<a>", {
                                                    class: "dropdown-item",
                                                    text: this.Nombre
                                                });
                                            linka.attr("href", obtenBase() + this.URL);

                                            link.append(linka);
                                        }
                                        divLink.append(link);

                                    });

                                    $(li).append(divLink);
                                }
                                else if (this.SubMenus.length > 0) {
                                    var ulsubmenu = $("<ul>", {
                                        class: "dropdown-menu"
                                    });

                                    $(this.SubMenus).each(function () {
                                        var submenuli = $("<li>", {
                                            class: "dropdown-submenu"
                                        });

                                        var submenua = $("<a>", {
                                            class: "dropdown-item dropdown-toggle",
                                            text: this.NombreSubMenu
                                        });
                                        submenua.attr("href", "#");
                                        submenuli.append(submenua);

                                        var submenulu = $("<ul>", {
                                            class: "dropdown-menu"
                                        });
                                        submenuli.append(submenulu);

                                        $(this.Links).each(function () {
                                            var submenulisa = $("<li>");
                                            var subMenu = $("<a>", {
                                                class: "dropdown-item",
                                                text: this.Nombre
                                            });
                                            subMenu.attr("href", obtenBase() + this.URL);

                                            submenulisa.append(subMenu);
                                            submenulu.append(submenulisa);
                                        });

                                        ulsubmenu.append(submenuli);    
                                    });

                                    $(li).append(ulsubmenu);
                                }

                                menuContainer.append(li);
                            });
                        }
                    }
                    else {
                        $(".navbar-brand").hide();
                    }
                },
                error: function (data) {
                    ShowNotification('error', 'Datos incorrectos.', data.statusText, true);
                }
            });
        }
        catch (ex) {
            return false;
        }
    }

});

//function multinivel(element, e) {
//    $(element).next('div').toggle();
//    e.stopPropagation();
//    e.preventDefault();
//}