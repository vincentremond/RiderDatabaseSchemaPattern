namespace RiderDatabaseSchemaPattern

open System.Text.RegularExpressions

[<RequireQualifiedAccess>]
module PatternBuilder =

    let buildDatabasePattern = ".*"

    let schemaPattern =
        let shouldNotMachAny = [
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

        let pattern = "^(?!(db_|INFORMATION_SCHEMA$|sys$|guest$)).*"

        let regex = Regex(pattern)

        for value in shouldNotMachAny do
            let isMatch = regex.IsMatch(value)

            if isMatch then
                failwithf $"The value '%s{value}' should not match the pattern '%s{pattern}'"

        pattern.Replace("|", @"\|")

    let build () =
        let databasePattern = buildDatabasePattern
        let schemaPattern = schemaPattern
        $"{{{databasePattern}}}:{{{schemaPattern}}}"
