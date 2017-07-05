#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <ctype.h>

#define NUM_CLASSES 4

int main() {

    const char *classNames[NUM_CLASSES] = {
        "Binary",
        "Grouping",
        "Literal",
        "Unary"
    };

    const char *classes[NUM_CLASSES] = {
        "Binary:Expr left, Token op, Expr right",
        "Grouping:Expr exp",
        "Literal:object val",
        "Unary:Token op, Expr right"
    };

    FILE *vp;
    vp = fopen("./generated/IVisitor.cs", "w+");

    fprintf(vp, "using System;\n");
    fprintf(vp,"\n");

    fprintf(vp, "namespace Lox\n");
    fprintf(vp, "{\n");
    fprintf(vp, "\tpublic interface IVisitor\n");
    fprintf(vp, "\t{\n");

    for(int i = 0; i < NUM_CLASSES; i++)
    {
        char* classDef = strdup(classes[i]);
        char* className = strtok(classDef, ":");
        char* args = strtok(NULL, ":");
        free(classDef);

        char* classLower = strdup(className);
        classLower[0] = tolower(className[0]);
        fprintf(vp, "\t\tvoid Visit(%s %s);\n", className, classLower);

        char fileName[40] = "./generated/";
        strcat(fileName, className);
        strcat(fileName, ".cs");

        printf("Generating %s with args %s in file %s\n", className, args, fileName);        

        FILE *fp;
        fp = fopen(fileName, "w+");

        fprintf(fp, "using System;\n");
        fprintf(fp, "\nnamespace Lox\n");
        fprintf(fp, "{\n");
        fprintf(fp, "\tpublic class %s : Expr\n", className);
        fprintf(fp, "\t{\n");
        fprintf(fp, "\t\tpublic %s (%s)\n", className, args);
        fprintf(fp, "\t\t{\n");

        const char *fields[10];
        int fieldCount = 0;
        
        char* argBuf = strdup(args);
        char* argToken = strtok(argBuf, ",");
        while(argToken != NULL)
        {
            fields[fieldCount++] = argToken;

            char* endPointer;
            char* tokenBuf = strdup(argToken);
            char* token = strtok_r(tokenBuf, " ", &endPointer);
            token = strtok_r(NULL, " ", &endPointer);
            fprintf(fp, "\t\t\tthis.%s = %s;\n", token, token);            
            argToken = strtok(NULL, ",");
        }
        free(argBuf);

        fprintf(fp, "\t\t}\n");
        fprintf(fp, "\n");
        
        for(int j = 0; j < fieldCount; j++)
        {
            fprintf(fp, "\t\tpublic %s { get; set; }\n", fields[j]);
        }

        fprintf(fp, "\n");
        fprintf(fp, "\t\tpublic override void Accept(IVisitor visitor)\n");
        fprintf(fp, "\t\t{\n");
        fprintf(fp, "\t\t\tvisitor.Visit(this);\n");
        fprintf(fp, "\t\t}\n");

        fprintf(fp, "\t}\n");
        fprintf(fp, "}\n");
        fclose(fp);
    }

    fprintf(vp, "\t}\n");
    fprintf(vp, "}\n");
    fclose(vp);    
}