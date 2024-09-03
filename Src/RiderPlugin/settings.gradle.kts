
pluginManagement {
    // Provide repositories to resolve plugins
    repositories {
        maven { setUrl("https://cache-redirector.jetbrains.com/plugins.gradle.org") }
        maven { setUrl("https://cache-redirector.jetbrains.com/maven-central") }
        maven { setUrl("https://cache-redirector.jetbrains.com/dl.bintray.com/kotlin/kotlin-eap") }
    }
}

val RiderPluginProjectName: String by settings

rootProject.name = RiderPluginProjectName
