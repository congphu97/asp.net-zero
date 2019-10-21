$(function () {
    var _uiCustomizationSettingsService = abp.services.app.uiCustomizationSettings;

    var quickSidebarOffCanvas = new KTOffcanvas('kt_theme_selection_panel', {
        baseClass: 'kt-demo-panel',
        closeBy: 'kt-theme_selection_panel_close',
        toggleBy: 'kt_theme_selection_panel_toggle'
    });

    // todo: should we handle this as well ?
    // run once on first time dropdown shown
    //quickSidebarOffCanvas.one('afterShow', function () {
    //    mApp.block($('#kt_theme_selection_panel'));

    //    setTimeout(function () {
    //        mApp.unblock($('#kt_theme_selection_panel'));
    //        $('#kt_theme_selection_panel').find('.kt-theme-selection-panel__content').removeClass('kt-hide');
    //    }, 1000);
    //});

    $('#kt_theme_selection_panel .m-scrollable').slimScroll({ destroy: true });
    $('#kt_theme_selection_panel .m-scrollable').slimScroll({
        height: $(window).height() - 20
    });

    $('.theme-selection-link').click(function () {
        var theme = $(this).data('theme');
        _uiCustomizationSettingsService.changeThemeWithDefaultValues(theme).done(function () {
            window.location.reload();
        });
    });
});