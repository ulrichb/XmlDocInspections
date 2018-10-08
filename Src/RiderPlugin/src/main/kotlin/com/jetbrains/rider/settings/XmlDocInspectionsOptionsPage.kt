package com.jetbrains.rider.settings

import com.jetbrains.rider.settings.simple.SimpleOptionsPage

class XmlDocInspectionsOptionsPage : SimpleOptionsPage("XML Doc Inspections", XmlDocInspectionsOptionsPage::class.simpleName!!) {

    override fun getId(): String {
        return "preferences." + this.pageId;
    }
}
