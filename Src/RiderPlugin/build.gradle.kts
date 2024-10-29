plugins {
    id("java")
    alias(libs.plugins.kotlinJvm)
    id("org.jetbrains.intellij.platform") version "2.0.0-beta5"   // https://github.com/JetBrains/intellij-platform-gradle-plugin/releases
    id("me.filippov.gradle.jvm.wrapper") version "0.14.0"   // https://plugins.gradle.org/plugin/me.filippov.gradle.jvm.wrapper
}

val ResharperPluginProjectName: String by project
val ReSharperVersionIdentifier: String by project
val BuildConfiguration: String by project
val ProductVersion: String by project

allprojects {
    repositories {
        maven { setUrl("https://cache-redirector.jetbrains.com/maven-central") }
    }
}

repositories {
    intellijPlatform {
        defaultRepositories()
        jetbrainsRuntime()
    }
}

tasks.wrapper {
    gradleVersion = "8.8"
    distributionType = Wrapper.DistributionType.ALL
    distributionUrl = "https://cache-redirector.jetbrains.com/services.gradle.org/distributions/gradle-${gradleVersion}-all.zip"
}

version = extra["PluginVersion"] as String

tasks.processResources {
    from("dependencies.json") { into("META-INF") }
}

sourceSets {
    main {
        java.srcDir("src/rider/main/java")
        kotlin.srcDir("src/rider/main/kotlin")
        resources.srcDir("src/rider/main/resources")
    }
}

tasks.compileKotlin {
    kotlinOptions { jvmTarget = "17" }
}

dependencies {
    intellijPlatform {
        rider(ProductVersion)
        jetbrainsRuntime()
        instrumentationTools()
    }
}

tasks.runIde {
    // Match Rider's default heap size of 1.5Gb (default for runIde is 512Mb)
    maxHeapSize = "1500m"
}

tasks.patchPluginXml {
    val changelogText = file("${rootDir}/../../History.md").readText()
    val changelogMatches = Regex("(?s)###(.+?)###(.+?)(?=###|\$)").findAll(changelogText)

    changeNotes = changelogMatches.map {
        val versionTitle = it.groups[1]!!.value
        val versionText = it.groups[2]!!.value.replace("(?s)\r?\n".toRegex(), "<br />\n")
        "<b>$versionTitle</b>$versionText"
    }.take(10).joinToString("")
}

tasks.prepareSandbox {
    from("${rootDir}/../${ResharperPluginProjectName}/bin/${ReSharperVersionIdentifier}/${BuildConfiguration}") {
        include("${ResharperPluginProjectName}*")
        into("${rootProject.name}/dotnet")
    }
}
