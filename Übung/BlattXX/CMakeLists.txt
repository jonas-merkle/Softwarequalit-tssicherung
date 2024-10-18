# 1. Set sub-project name
# ------------------------------------------------------------------------------------------------------
SET(PROJ_NAME UebungsblattXX)
PROJECT(${PROJ_NAME})
MESSAGE(STATUS "· ${PROJ_NAME} document added!")

# 2. Set pandoc variables for the sub-project
# ------------------------------------------------------------------------------------------------------
SET(${PROJ_NAME}_DOCS "BlattXX.md")
SET(${PROJ_NAME}_RESOURCES resources src)
SET(${PROJ_NAME}_OUTPUT_NAME ${PROJ_NAME}_Jonas-Merkle)

# 3. Setup pandoc to create the document
# ------------------------------------------------------------------------------------------------------
MESSAGE(STATUS "· ${PROJ_NAME} setup pandoc for pdf!")
ADD_DOCUMENT(
    TARGET              ${PROJ_NAME}_pdf
    OUTPUT_FILE         ${${PROJ_NAME}_OUTPUT_NAME}.pdf
    OUTPUT_FORMAT       pdf
    SOURCES             ${${PROJ_NAME}_DOCS}
    RESOURCE_DIRS       ${${PROJ_NAME}_RESOURCES}
    PRODUCT_DIRECTORY   output/${PROJ_NAME}
    PANDOC_DIRECTIVES   -f markdown-implicit_figures
)

# 4. Setup pandoc to create the document
# ------------------------------------------------------------------------------------------------------
MESSAGE(STATUS "· ${PROJ_NAME} setup pandoc for html!")
ADD_DOCUMENT(
    TARGET              ${PROJ_NAME}_html
    OUTPUT_FILE         ${${PROJ_NAME}_OUTPUT_NAME}.html
    OUTPUT_FORMAT       html5
    SOURCES             ${${PROJ_NAME}_DOCS}
    RESOURCE_DIRS       ${${PROJ_NAME}_RESOURCES}
    PRODUCT_DIRECTORY   output/${PROJ_NAME}
    PANDOC_DIRECTIVES   --mathml
)

# 5. Setup copy & zip of output
# ------------------------------------------------------------------------------------------------------
MESSAGE(STATUS "· ${PROJ_NAME} setup copy & zip of output!")
add_custom_command(
    TARGET ${PROJ_NAME}_pdf
    POST_BUILD
    COMMAND cp -R ${CMAKE_CURRENT_SOURCE_DIR}/* ${CMAKE_BINARY_DIR}/output/${PROJ_NAME}
)
add_custom_command(
    TARGET ${PROJ_NAME}_pdf
    POST_BUILD
    COMMAND cd ${CMAKE_BINARY_DIR}/output/${PROJ_NAME} && zip -q -9 -r ${${PROJ_NAME}_OUTPUT_NAME}.zip ./*
)