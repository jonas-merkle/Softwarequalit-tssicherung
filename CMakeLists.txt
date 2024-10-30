# 1. Set CMake minimum version
# ------------------------------------------------------------------------------------------------------
cmake_minimum_required(VERSION 3.25)

# 2. Add 'cmake-pandocology' to CMake
# ------------------------------------------------------------------------------------------------------
MESSAGE(STATUS "Adding CMake modules...")
LIST(APPEND CMAKE_MODULE_PATH "${CMAKE_SOURCE_DIR}/cmake/Modules/cmake-pandocology")
INCLUDE(pandocology)
INCLUDE(cmspandoc)

# 3. Define the main project
# ------------------------------------------------------------------------------------------------------
SET(PROJ_MAIN_NAME "Softwarequalitaetssicherung")            # This ID must be unique
PROJECT(${PROJ_MAIN_NAME}-main NONE)                        # Create solution project
MESSAGE(STATUS "HTML generation for ${PROJ_MAIN_NAME} ...")

# 4. Configure pandoc
# ------------------------------------------------------------------------------------------------------
CONFIGUREPANDOCOLOGY()
SET(${PROJ_MAIN_NAME}_PATH_MAIN ${CMAKE_SOURCE_DIR} CACHE PATH "This directory contains initial Path" )

# 5. Adds the subdirectories
# ------------------------------------------------------------------------------------------------------
MESSAGE(STATUS "Adding documents in the sub directories...")
ADD_SUBDIRECTORY(Übung/Blatt02)
ADD_SUBDIRECTORY(Übung/Blatt01)
ADD_SUBDIRECTORY(Übung/Blatt00)

# 6. Setup pandoc to create the document
# ------------------------------------------------------------------------------------------------------
MESSAGE(STATUS "${PROJ_MAIN_NAME} setup pandoc for html!")
ADD_DOCUMENT(
    TARGET              ${PROJ_MAIN_NAME}_html
    OUTPUT_FILE         index.html
    OUTPUT_FORMAT       html5
    SOURCES             README.md
    PRODUCT_DIRECTORY   output
    PANDOC_DIRECTIVES   --mathml
)

# 7. The End
# ------------------------------------------------------------------------------------------------------
MESSAGE(STATUS "HTML generation for ${${PROJ_MAIN_NAME}-main} done!")