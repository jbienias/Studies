#include<stdio.h>
#include<stdlib.h>
#include<string.h>
#include<unistd.h>
#include<ctype.h>
#include<libpq-fe.h>

PGconn *conn;
//PGresult *result;

int MAX_STR_LEN = 100; //native strlen max
int CMD_STR_LEN = 200; //native strlen for SQL CMD's
int VARCHAR_MAX = 10; //native varchar max, used for checking strlen mainly

typedef struct Data{
    char *field;
}data;

void doSQL(PGconn *conn, char *command)
{
	PGresult *result;
	printf("%s\n", command);
	result = PQexec(conn, command);
	printf("# status is     : %s\n", PQresStatus(PQresultStatus(result)));
	printf("# rows affected: %s\n", PQcmdTuples(result));
	printf("# result error message: %s\n", PQresultErrorMessage(result));
	switch(PQresultStatus(result)) 
	{
		case PGRES_TUPLES_OK:
		{
			int n = 0, m = 0;
			int nrows   = PQntuples(result);
			int nfields = PQnfields(result);
			printf("+ number of rows returned   = %d\n", nrows);
			printf("+ number of fields returned = %d\n", nfields);
			for(m = 0; m < nrows; m++) 
			{
				for(n = 0; n < nfields; n++)
				printf(" %s = %s", PQfname(result, n),PQgetvalue(result,m,n));
				printf("\n");
			}
		}
	}
	PQclear(result);
}

void doSQL_printTuples(PGconn *conn, char *command)
{
	PGresult *result;
	printf("%s\n", command);
	result = PQexec(conn, command);
	switch(PQresultStatus(result)) 
	{
		case PGRES_TUPLES_OK:
		{
			int n = 0, m = 0;
			int nrows   = PQntuples(result);
			int nfields = PQnfields(result);
			printf("+ number of rows returned   = %d\n", nrows);
			printf("+ number of fields returned = %d\n", nfields);
			for(m = 0; m < nrows; m++) 
			{
				for(n = 0; n < nfields; n++)
				printf(" %s = %s", PQfname(result, n),PQgetvalue(result,m,n));
				printf("\n");
			}
		}
	}
	PQclear(result);
}

void doSQL_noinfo(PGconn *conn, char *command)
{
	PGresult *result;
	result = PQexec(conn, command);
	PQclear(result);
}

void doSQL_cmdonly(PGconn *conn, char *command)
{
	PGresult *result;
	printf("%s\n", command);
	result = PQexec(conn, command);
	PQclear(result);
}

void FileWithoutExtension(char *filename)
{
    char * cut;
    cut = strtok (filename,".");
    filename = cut;
}

void InitializeArray(int rows, int columns, data arr[][columns])
{
    int i,j;
    for(i=0;i<rows;i++)
    {
        for(j=0;j<columns;j++)
        {
            arr[i][j].field = malloc(sizeof(char) * MAX_STR_LEN);
            arr[i][j].field = "NULL"; //or NULL
        }
    }
}

void InitializeArray2(int rows, data arr[])
{
    int i,j;
    for(i=0;i<rows;i++)
    {
		arr[i].field = malloc(sizeof(char) * MAX_STR_LEN);
		arr[i].field = "NULL"; //or NULL
    }
}

void InitializeVarcharArray(int columns, int array[])
{
    int i;
    for(i=0; i<columns; i++)
        array[i] = VARCHAR_MAX;
}

void InitializeArrayOptions(int columns, int arr[])
{
    int i;
    for(i=0;i<columns;i++)
        arr[i] = 0;
}

int CheckColumns(int columns, data arr[][columns])
{
    int i;
    for(i=0; i<columns; i++)
    {
        if(arr[0][i].field == NULL)
            return 0;
    }
    return 1;
}

int FileToArray(int rows, int columns, data arr[][columns], char *filename)
{
    InitializeArray(rows, columns, arr);
    int i=0, j=0;
    FILE *file;
    file = fopen(filename,"r");
    if (!file)
    {
        printf("Cannot open file %s\n", filename);
        return 0;
    }
    char* tmp_field[rows];
    for(i=0; i<rows; i++)
    {
        tmp_field[i] = malloc(sizeof(char) * MAX_STR_LEN);
        fscanf(file, "%[^\n]%*c", tmp_field[i]); //with whitespace, or without whitespace -> %s 
        char *cut;
        cut = strtok (tmp_field[i],";");
        for(j=0; j<columns; j++)
        {
			if(cut == NULL)
			{
				arr[i][j].field = "NULL";
			}
			else
				arr[i][j].field = cut;
            cut = strtok (NULL, ";");
        }
    }
    fclose(file);
    if(CheckColumns(columns, arr) == 0)
    {
        printf("Columns of given file have NULL/whitespace names!\n");
        return 0;
    }
    return 1;
}

int FileInfo (int *row, int *column, int *max_row_len, char *filename)
{
    FILE *file;
    char ch;
    int max = 0;
    int rows=0, columns=0, row_len=0;
    file = fopen(filename,"r");
    if (!file)
    {
        printf("Cannot open file %s\n", filename);
        return 0;
    }
    int firstLine = 1;
    while ((ch=getc(file)) != EOF)
    {
        if (ch == ';' && firstLine == 1)
        {
            columns++;
        }
        else if (ch == '\n')
        {
            rows++;
            firstLine = 0;
            if(max < row_len)
            {
                max = row_len;
                row_len = 0;
            }
        }
        else
        {
            row_len++;
        }
    }
    fclose(file);
    rows++; columns++; row_len++;
    *row = rows;
    *column = columns;
    max += 5; //for pure insurance
    *max_row_len = max;
    return 1;
}

void GetLoginInfo(char *saveTo, char* login, char *password, char *db_name, int option)
{
    printf("Login > "); scanf("%s", login);
    password = getpass("Password > ");
	if(option == 1)
	{
		 printf("Database Name > "); scanf("%s", db_name);
	}
    strcpy(saveTo,"host=localhost ");
    strcat(saveTo, "port=5432 dbname=");
    strcat(saveTo, db_name);
    strcat(saveTo, " user=");
    strcat(saveTo, login);
    strcat(saveTo, " password=");
    strcat(saveTo, password);
}

void CreateCMD(char *saveTo, int rows, int columns, data arr[][columns], char *tableName)
{
    int i;
    strcpy(saveTo, "CREATE TABLE ");
    strcat(saveTo, tableName);
    strcat(saveTo, "(");
    strcat(saveTo, arr[0][0].field);
    strcat(saveTo, " VARCHAR(");
    char VARCHAR_MAX_STRING[MAX_STR_LEN];
    sprintf(VARCHAR_MAX_STRING, "%d", VARCHAR_MAX);
    strcat(saveTo, VARCHAR_MAX_STRING);
    if(columns == 1)
        strcat(saveTo, "))");
    else
        strcat(saveTo, "), ");
    for(i=1; i<columns; i++)
    {
        strcat(saveTo, arr[0][i].field);
        strcat(saveTo, " VARCHAR(");
        char VARCHAR_MAX_STRING[MAX_STR_LEN];
        sprintf(VARCHAR_MAX_STRING, "%d", VARCHAR_MAX);
        strcat(saveTo, VARCHAR_MAX_STRING);
		strcat(saveTo, "), ");
    }
	strcat(saveTo, "id_");
	strcat(saveTo, tableName);
	strcat(saveTo, "_serial SERIAL)");
}

void AlterColumnVarcharCMD(char *saveTo, int column_number, int columns, data arr[][columns],int varchar_array_info[], char *tableName)
{
    strcpy(saveTo, "ALTER TABLE ");
    strcat(saveTo, tableName);
    strcat(saveTo, " ALTER COLUMN ");
    strcat(saveTo, arr[0][column_number].field);
	strcat(saveTo, " TYPE VARCHAR(");
    char VARCHAR_MAX_STRING[MAX_STR_LEN];
    sprintf(VARCHAR_MAX_STRING, "%d", varchar_array_info[column_number]);
    strcat(saveTo, VARCHAR_MAX_STRING);
    strcat(saveTo, ")");
}

void lower_string(char s[]) 
{
   int c = 0;
   while (s[c] != '\0') {
      if (s[c] >= 'A' && s[c] <= 'Z') {
         s[c] = s[c] + 32;
      }
      c++;
   }
}

void AlterColumnsTableCMD(int columns, data arr[], char *tableName)
{
	int i;
	for(i=0; i<columns; i++)
	{
		char cmd[CMD_STR_LEN];
		strcpy(cmd,"ALTER TABLE ");
		strcat(cmd,tableName);
		strcat(cmd, " ADD COLUMN ");
		strcat(cmd, arr[i].field);
		strcat(cmd, " VARCHAR(");
		char VARCHAR_MAX_STRING[MAX_STR_LEN];
		sprintf(VARCHAR_MAX_STRING, "%d", VARCHAR_MAX);
		strcat(cmd, VARCHAR_MAX_STRING);
		strcat(cmd, ");");
		doSQL_cmdonly(conn, cmd);
	}
}

void AlterTableByFileCMD(int columns, data arr[][columns], char *tableName)
{
	int i, j;
	PGresult *query;
	char cmd[CMD_STR_LEN];		
	strcpy(cmd,"SELECT * FROM ");
	strcat(cmd,tableName);
	strcat(cmd,";");
	query=PQexec(conn,cmd);
	int size = PQnfields(query);
	data table_columns[size];
	data file_columns[columns];
	InitializeArray2(size, table_columns);
	InitializeArray2(columns, file_columns);
	for(i=0; i<size; i++)
	{
		table_columns[i].field = PQfname(query,i);
	}
	for(i=0; i<columns ;i++)
	{
		lower_string(arr[0][i].field);
		file_columns[i].field = arr[0][i].field;
	}
	int helper[columns];
	int equal_counter = 0;
	int not_equal_counter;
	InitializeArrayOptions(columns, helper);
	for(i=0; i<columns;i++)
	{
		for(j=0;j<size;j++)
		{
			if(strcmp(file_columns[i].field,table_columns[j].field) == 0)
			{
				helper[i] = 1;
				equal_counter++;
			}
		}
	}
	not_equal_counter = columns - equal_counter;
	data new_columns[not_equal_counter];
	InitializeArray2(not_equal_counter, new_columns);
	j=0;
	for(i=0; i<columns;i++)
	{
		if(helper[i] == 0)
		{
			new_columns[j].field = file_columns[i].field;
			j++;
		}
	}
	j=0;
	if(not_equal_counter > 0)
		AlterColumnsTableCMD(not_equal_counter, new_columns, tableName);
}

void InsertTableCMD(char *saveTo, int row, int columns, data arr[][columns], int varchar_array_info[], char *tableName)
{
    int i;
    for(i=0; i<columns; i++)
    {
        if(varchar_array_info[i] > 0 && strlen(arr[row][i].field) > varchar_array_info[i]) //albo samo strlen
        {
            char tmp[CMD_STR_LEN];
            varchar_array_info[i] = strlen(arr[row][i].field);
            AlterColumnVarcharCMD(tmp, i, columns, arr, varchar_array_info, tableName);
            doSQL_cmdonly(conn, tmp);
        }
    }
    strcpy(saveTo, "INSERT INTO ");
    strcat(saveTo, tableName);
    strcat(saveTo, "(");
    for(i=0; i<columns; i++)
    {
        strcat(saveTo, arr[0][i].field);
        if(i == columns-1)
            strcat(saveTo, ")");
        else
            strcat(saveTo, ", ");
    }
    strcat(saveTo, " VALUES('");
    for(i=0; i<columns; i++)
    {
        if(arr[row][i].field == NULL)
        {
            strcat(saveTo, "NULL");
        }
        else
            strcat(saveTo, arr[row][i].field);
        if(i == columns-1)
            strcat(saveTo, "')");
        else
            strcat(saveTo, "', '");
    }
}

void SelectAllCMD(char *saveTo, char *tableName)
{
	strcpy(saveTo,"SELECT * FROM ");
	strcat(saveTo,tableName);
	strcat(saveTo, ";");
}

void SelectOrderByCMD(char *saveTo, char *tableName, int columns, int arr[], data names[])
{
	int i, j;
	int countZeros = 0;
	int countOption = 0;
	for(i=0; i<columns; i++)
	{
		if(arr[i] == 0)
		{
			countZeros++;
		}
		else
			countOption++;
	}
	if(countZeros == columns)
	{
		strcpy(saveTo, "SELECT * FROM ");
		strcat(saveTo, tableName);
		strcat(saveTo, ";");
		return;
	}
	strcpy(saveTo, "SELECT * FROM ");
	strcat(saveTo, tableName);
	strcat(saveTo, " ORDER BY ");
	for(i=0; i<columns; i++)
	{
		if(arr[i] == 1)
		{
			strcat(saveTo, names[i].field);
			strcat(saveTo, " ASC");
			if(countOption == 1)
			{
				strcat(saveTo, ";");
			}
			else 
			{
				strcat(saveTo,", ");
				countOption--;
			}
		}
		else if(arr[i] == 2)
		{
			strcat(saveTo, names[i].field);
			strcat(saveTo, " DESC");
			if(countOption == 1)
			{
				strcat(saveTo, ";");
			}
			else 
			{
				strcat(saveTo,", ");
				countOption--;
			}
		}
	}
}

void SetColumnSortingOptions(int columns, int arr[])
{//1-> ASC, 2-> DESC, 0-> do not sort, default
    int j, go=1;
    while(go)
    {
        system("clear");
        printf("Current state of options: ");
        for(j=0; j<columns; j++)
        {
           printf("%d ", arr[j]);
        }
        printf("\n");
        printf("Enter column number (1-%d) you want to sort table by > ", columns);
        int i;
        scanf("%d", &i);
        i--;
        if(i<0 || i>columns-1)
        {
            printf("Column number %d does not exist\n", i+1);
        }
        else
        {
            printf("Enter 1 (ASC) or 2 (DESC) > ");
            scanf("%d", &arr[i]);
            if(arr[i]<=0 || arr[i] > 2)
            {
                printf("Unknown sorting option! Setting to default: 0 (without sort)\n");
                arr[i] = 0;
            }
        }
		printf("Updated state of options: ");
        for(j=0; j<columns; j++)
        {
           printf("%d ", arr[j]);
        }
        printf("\nContinue assigning column sort? (1 - yes/ !1 - no) > ");
        int option;
        scanf("%d", &option);
        if(option != 1)
        {
            go = 0;
        }
    }
}

void tableToHTMLFile(PGconn *conn, int arg_count, char *arg[])
{
	FILE *file;
    file = fopen(arg[arg_count-1],"w+");
    if (!file)
    {
        printf("Cannot open file %s\n", arg[arg_count-1]);
        return;
    }
	int i;
	PGresult *query;
	char *error;
	char cmd[CMD_STR_LEN];
	char css[CMD_STR_LEN];
	strcpy(css, "table, th, td { border: 1px solid black;} table {border-collapse: collapse;} th {background-color: #00CCFF");
	fprintf(file,"<!DOCTYPE html>\n<html>\n<head>\n<meta charset=\"utf-8\">\n<title>%s</title>\n", arg[arg_count-1]);
	fprintf(file,"<style>%s</style>", css);
	fprintf(file,"</head>\n<body>\n");
 
	for(i=2;i<arg_count-1;i++)
	{
		strcpy(cmd,"SELECT * FROM ");
		strcat(cmd,arg[i]);
		strcat(cmd,";");
		query=PQexec(conn,cmd);
		error = PQresultErrorMessage(query);
 
		if(strlen(error)>0) 
		{
			fprintf(file,"<div>%s</div>\n",error);
			printf("ERROR: Table \"%s\" does not exist in database \"%s\"!\n", arg[i], arg[2]);
		}
		
		if(PQresultStatus(query) == PGRES_TUPLES_OK) 
		{
			int j;
			char cmd[CMD_STR_LEN];
			int size = PQnfields(query);
			int columns_sort[size];
			InitializeArrayOptions(size, columns_sort);
			data column_names[size];
			InitializeArray2(size, column_names);
			for(j = 0; j<size; j++)
			{
				column_names[j].field = PQfname(query,j);
			}
			int option;
			printf("Assing order-by options on table %d. \"%s\" ? (1 - yes/ !1 - no) > ", i-1, arg[i]);
			scanf("%d", &option);
			if(option == 1)
			{
				SetColumnSortingOptions(size, columns_sort);
				SelectOrderByCMD(cmd, arg[i], size, columns_sort, column_names);
			}
			else
			{
				strcpy(cmd,"SELECT * FROM ");
				strcat(cmd,arg[i]);
				strcat(cmd,";");
			}
			query = PQexec(conn, cmd);
			int n,m;
			int NoRows = PQntuples(query);
			int NoFields = PQnfields(query);
			fprintf(file,"<h2>%s</h2>\n",arg[i]);
			fprintf(file,"<table>\n");
			fprintf(file,"<tr class=\"title\">\n");
			for(n = 0; n < NoFields; n++)
			{
				fprintf(file,"<th>%s</th>", PQfname(query,n));
			}
			fprintf(file,"</tr>\n");
 
			for(m = 0; m < NoRows; m++)
			{
				fprintf(file,"<tr class=\"a\">\n");
				for(n = 0; n < NoFields; n++)
				{
					fprintf(file,"<td class=\"val\">%s</td>\n", PQgetvalue(query,m,n));
				}
				fprintf(file,"</tr>\n");
			}
			fprintf(file,"</table>\n");
		}
		PQclear(query);
	}
	fprintf(file,"</body>\n</html>\n");
	fclose(file);
}

int main(int arg_count, char* arg[])
{
    if(arg_count == 2)
    {
		system("clear");
		char login[MAX_STR_LEN], password[MAX_STR_LEN], db_name[MAX_STR_LEN], filename[MAX_STR_LEN], filename_noext[MAX_STR_LEN];
        char serv_info[MAX_STR_LEN];
        int rows, columns, max;
        strcpy(filename, arg[1]);
        strcpy(filename_noext, filename);
        FileWithoutExtension(filename_noext);
        GetLoginInfo(serv_info, login, password, db_name, 1);
        if(FileInfo(&rows, &columns, &max, filename) == 0)
        {
            return EXIT_FAILURE;
        }
        if(max > MAX_STR_LEN)
        {
            MAX_STR_LEN = max;
        }
        int varchar_column_info[columns];
        InitializeVarcharArray(columns, varchar_column_info);
        data array[rows][columns];
        if( FileToArray(rows, columns, array, filename) == 0)
        {
            return EXIT_FAILURE;
        }
		printf("\nConnecting...\n");
		conn = PQconnectdb(serv_info);
		if(PQstatus(conn) == CONNECTION_OK) 
		{
			printf("Connection established!\n");
			char cmd[CMD_STR_LEN];
			char *error;
			CreateCMD(cmd, rows, columns, array, filename_noext);		
			PGresult *query;
			query = PQexec(conn,cmd);
			error = PQresultErrorMessage(query);

			if(strlen(error) > 0)
			{
				system("clear");
				printf("Table \"%s\" ALREADY EXISTS in database : \"%s\"!\n", filename_noext, db_name);
				int i;
				AlterTableByFileCMD(columns, array, filename_noext);
				SelectAllCMD(cmd, filename_noext);
				query = PQexec(conn,cmd);
				int NoFields = PQnfields(query);
				int varchars_from_existing_table[NoFields];
				InitializeArrayOptions(NoFields, varchars_from_existing_table);
				//http://www.postgresql-archive.org/Libpq-PQftype-PQfsize-td2457316.html
				for(i=0; i < NoFields; i++)
				{
					int d = PQfmod(query, i);
					d -= 4;
					varchars_from_existing_table[i] = d;
				}
				for(i=1; i<rows; i++)
				{
					InsertTableCMD(cmd, i, columns, array, varchars_from_existing_table, filename_noext);
					doSQL_cmdonly(conn, cmd);
				}
				SelectAllCMD(cmd, filename_noext);
				doSQL_printTuples(conn, cmd);
				PQclear(query);
				PQfinish(conn);
				return EXIT_SUCCESS;				
			}
			else 
			{
				system("clear");
				printf("Table \"%s\" does NOT EXIST in database : \"%s\"!\n", filename_noext, db_name);
				printf("Creating proper table...\n");
				doSQL_cmdonly(conn, cmd); 
				int i;
				printf("Adding records from file \"%s\"...\n", filename);
				for(i=1; i<rows; i++)
				{
					InsertTableCMD(cmd, i, columns, array, varchar_column_info, filename_noext);
					doSQL_cmdonly(conn, cmd);
				}
				SelectAllCMD(cmd, filename_noext);
				doSQL_printTuples(conn, cmd);
				PQclear(query);
				PQfinish(conn);
				return EXIT_SUCCESS;
			}
		}
		else
		{
			system("clear");
			printf("ERROR");
			printf("\nServer information is invalid or given database is not available for your account.\n\n");
			return EXIT_FAILURE;
		}
    }

    else if(arg_count > 3)
    {
		system("clear");
		char login[MAX_STR_LEN], password[MAX_STR_LEN];
		char serv_info[MAX_STR_LEN];
		GetLoginInfo(serv_info, login, password, arg[1], 0);
		printf("\nConnecting...\n");
		conn = PQconnectdb(serv_info);
		if(PQstatus(conn) == CONNECTION_OK) 
		{
			system("clear");
			printf("Connection established!\n");
			tableToHTMLFile(conn, arg_count, arg);
			printf("Printing given database tables into %s file...\n", arg[arg_count-1]);
			printf("\n...Done!\n\n");
			PQfinish(conn);
			return EXIT_SUCCESS;
		}
		else
		{
			system("clear");
			printf("ERROR");
			printf("\nServer information is invalid or given database is not available for your account.\n\n");
			return EXIT_FAILURE;
		}
    }
	else
	{
		system("clear");
		printf("This program needs arguments : \n./name file.ext or \n./name db_name table_name1.....table_name2 file_to_save.ext\n\n");
		return EXIT_FAILURE;		
	}
	return EXIT_SUCCESS;
}
