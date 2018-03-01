### vNext ###
- ReSharper and Rider 2018.1 support

### 2.3.0 ###
- Rider 2017.3 support

### 2.2.0 ###
- ReSharper 2017.3 support

### 2.1.0 ###
- Added support for Rider 2017.2

### 2.0.1 ###
- Added exclusion for "synthetic" code elements (fixes analyzed "fields" in Razor files, see #12)

### 2.0.0 ###
- Improved options page (added clear distinction between inclusion and exclusion rules) => this means a breaking change for "Exclude constructors" and "Exclude members which override super/base members" settings
- Add attributes inclusion setting (configured by default to `[PublicAPI]`)
- ReSharper 2017.2 support

### 1.8.0 ###
- Added a quick fix action on XML Doc warnings to generate doc templates
- Added a configuration option to include/exclude constructors
- ReSharper 2017.1 support

### 1.7.0 ###
- ReSharper 2016.3 support

### 1.6.0 ###
- ReSharper 2016.2 support

### 1.5.0 ###
- Added suppression for member declarations in XAML files (fixes issue #6)
- Added new setting to allow to ignore overriding type members (implements #3)

### 1.4.0 ###
- ReSharper 2016.1 support
- "XML Doc Inspections" page in ReSharper options is now searchable

### 1.3.0 ###
- ReSharper 10.0 support
- Dropped ReSharper 8.2 support

### 1.2.1 ###
- Fixed extension meta data to enable running it in InspectCode (ReSharper Command Line Tools)

### 1.2 ###
- ReSharper 9.2 support

### 1.1 ###
- ReSharper 9.1 support
