import os
import sys

os.chdir(os.path.dirname(os.path.abspath(sys.argv[0])))

def transform_cs_file(path):
	with open(path, "r", encoding="utf-8") as file:
		lines = file.readlines()

	using_lines = []
	rest_lines = []
	state = "using"

	for line in lines:
		if state == "using" and line.strip().startswith("using"):
			using_lines.append(line)
		else:
			state = "body"
			rest_lines.append(line)

	indented_rest = ["\t" + line if line.strip() else line for line in rest_lines]

	new_lines = (
		using_lines
		+ ["\n", "namespace Neeko {\n"]
		+ indented_rest
		+ ["\n}\n"]
	)

	with open(path, "w", encoding="utf-8") as file:
		file.writelines(new_lines)

for root, _, files in os.walk("."):
	for file in files:
		if file.endswith(".cs"):
			full_path = os.path.join(root, file)
			transform_cs_file(full_path)
