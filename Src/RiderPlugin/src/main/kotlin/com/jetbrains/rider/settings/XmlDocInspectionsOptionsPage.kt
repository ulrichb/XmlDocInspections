package com.jetbrains.rider.settings

import com.jetbrains.rider.settings.simple.SimpleOptionsPage

class XmlDocInspectionsOptionsPage : SimpleOptionsPage(
        name = "XML Doc Inspections",
        pageId = XmlDocInspectionsOptionsPage::class.simpleName!!
) {

    override fun getId(): String {
        return "preferences." + this.pageId;
    }
}
