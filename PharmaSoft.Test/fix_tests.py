import os
import re

test_dir = r"c:\EnelP4\Programación IV\PharmaSoft\PharmaSoft.Test"
for filename in os.listdir(test_dir):
    if filename.endswith("ServiceTests.cs") or filename == "MedicamentosServiceTest.cs":
        filepath = os.path.join(test_dir, filename)
        with open(filepath, 'r', encoding='utf-8') as f:
            content = f.read()

        # Find the Inserta test block
        # It looks like: var nuevo = Create...(id: 10, ...); ... var saved = await context.Entidades.FirstOrDefaultAsync(x => x.Prop == 10);
        # We need to change id: 10 to id: 0
        
        # Replace id: 10 with id: 0 in the "nuevo" creation
        content = re.sub(r'(var nuevo = Create\w+\(.*?)id:\s*10', r'\g<1>id: 0', content)
        
        # Replace == 10 with something else? Actually, if we just find "== 10" and replace with "== 2" ?
        # No, EF might assign 2 or something else. We can replace == 10 with > 0.
        # Wait, if we use FirstOrDefaultAsync(x => x.Prop > 0) and there's only 1 seeded (which is 1) and this one (which is 2). We can just order by descending!
        # Even simpler: context.Entidades.OrderByDescending(x => x.Prop).FirstOrDefaultAsync() but we don't know the Prop.
        
        # What if we just replace x => x.PropId == 10 with x => x.PropId == nuevo.PropId?
        # We can capture the property name: (\w+)\s=>\s\1\.(\w+)\s==\s10 -> \g<1> => \g<1>.\g<2> == nuevo.\g<2>
        content = re.sub(r'(\w+)\s*=>\s*\1\.(\w+)\s*==\s*10\b', r'\g<1> => \g<1>.\g<2> == nuevo.\g<2>', content)
        
        with open(filepath, 'w', encoding='utf-8') as f:
            f.write(content)
        print(f"Updated {filename}")
