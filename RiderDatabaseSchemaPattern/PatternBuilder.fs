namespace RiderDatabaseSchemaPattern

open System.Text.RegularExpressions

[<RequireQualifiedAccess>]
module PatternBuilder =

    let validatePattern pattern (shouldNotMatchValues: string list) =

        let regex = Regex(pattern)

        for value in shouldNotMatchValues do
            let isMatch = regex.IsMatch(value)

            if isMatch then
                failwithf $"The value '%s{value}' should not match the pattern '%s{pattern}'"

        pattern.Replace("|", @"\|")

    let buildDatabasePattern () =
        validatePattern "^(?!(tempdb$|master$|msdb$|model$)).*" [
            "tempdb"
            "master"
            "msdb"
            "model"
        ]

    let schemaPattern () =

        validatePattern "^(?!(db_|INFORMATION_SCHEMA$|sys$|guest$)).*" [
            "db_backupoperator"
            "db_datareader"
            "db_datawriter"
            "db_ddladmin"
            "db_denydatareader"
            "db_denydatawriter"
            "db_owner"
            "db_securityadmin"
            "INFORMATION_SCHEMA"
            "sys"
        ]

    let build () =
        let databasePattern = buildDatabasePattern ()
        let schemaPattern = schemaPattern ()
        $"{{{databasePattern}}}:{{{schemaPattern}}}"
