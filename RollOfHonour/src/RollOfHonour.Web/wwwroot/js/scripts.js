const tablist = $("[role='tablist']").find("[role='tab']");

function tabsClickHandler() {
  var thisTabId = $(this).attr("id");
  var thisTabList = $(this).parents("[role='tablist']").find("[role='tab']");

  thisTabList.each(function () {
    var thisTabPanel = $(
      "[role='tabpanel'][id='" + $(this).attr("aria-controls") + "']"
    );
    if ($(this).attr("id") == thisTabId) {
      thisTabPanel.removeClass("hide");
      $(this).attr("aria-selected", true).attr("tabindex", "0");
    } else {
      thisTabPanel.addClass("hide");
      $(this).attr("aria-selected", false).attr("tabindex", "-1");
    }
  });
}

function tabsHandler(event) {
  var index = tablist.index($(this));
  var numbTabs = tablist.length;
  var nextId;

  if (numbTabs > 1) {
    if (event.keyCode == 40 || event.keyCode == 39) {
      // DOWN or RIGHT
      nextId = tablist.eq(index + 1);

      if (index == numbTabs - 1) {
        // if it is the last not empty tab, then go to first not empty tab
        nextId = tablist.eq(0);
      }

      nextId.focus(); // focus on next tab
      nextId.click();
    }

    if (event.keyCode == 38 || event.keyCode == 37) {
      // UP or LEFT
      nextId = tablist.eq(index - 1);

      if (index == 0) {
        // if it is the last not empty tab, then go to first not empty tab
        nextId = tablist.eq(numbTabs - 1);
      }

      nextId.focus(); // focus on next tab
      nextId.click();
    }
  }
}

tablist.on("keydown", tabsHandler);
tablist.on("click", tabsClickHandler);

$("[role='tablist'] [role='tab'][aria-selected='true']").click();
